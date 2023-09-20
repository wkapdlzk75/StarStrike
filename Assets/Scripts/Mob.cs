using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mob : Unit
{
    public string mobName;
    public int score;
    public Sprite[] sprite;

    SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rb;

    public Player player;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;

        // 여기에 체력 설정해도 좋음

        if (mobName == "B")
        {
            animator = GetComponent<Animator>();
            Invoke("MoveStop", 3);
            return;
        }

        // 최초 몹 소환 2초 후 총알 발사, bulletFiringInterval초 마다 총알 발사
        InvokeRepeating("Fire", 2, bulletFiringInterval);
    }

    public void mobInit()
    {
        curHp = maxHp;

    }

    void MoveStop()
    {
        //if (!gameObject.activeSelf)
        //    return;
        rb.velocity = Vector2.zero;
        InvokeRepeating("BossRandomFire", 4, 6);
    }

    void BossRandomFire()
    {
        int ran = Random.Range(0, 4);
        switch (ran)
        {
            case 0:
                StartCoroutine(ShotCoroutine(10, 0.1f, FireFowardToPlayer));
                break;
            case 1:
                StartCoroutine(ShotCoroutine(1, 0, FireShotToPlayer));
                break;
            case 2:
                StartCoroutine(ShotCoroutine(10, 0.1f, FireArc, 1));
                break;
            case 3:
                StartCoroutine(ShotCoroutine(10, 0.1f, FireHalfAround, 1));
                break;
        }
    }

    void FireFowardToPlayer()
    {
        // 리스트나 배열로 해서 중복 제거
        if (player == null) return;
        Vector2 playerPos = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;

        List<Vector3> vectors = new List<Vector3>();
        vectors.Add(new Vector3(0.58f, -1.4f, 0));
        vectors.Add(new Vector3(0.87f, -1.4f, 0));
        vectors.Add(new Vector3(-0.58f, -1.4f, 0));
        vectors.Add(new Vector3(-0.87f, -1.4f, 0));

        for (int i = 0; i < vectors.Count; i++)
        {
            var tempIndex = i;

            ObjectManager.Instance.GetRangedObject("MobBulletC", (poolingBullet) =>
            {
                poolingBullet.transform.position = transform.position + vectors[tempIndex];
                poolingBullet.transform.rotation = Quaternion.Euler(0, 0, angle - 270);

                var bullet = poolingBullet.GetComponent<Bullet>();
                bullet.nameBullet = "MobBulletC";
                bullet.damage = damage;

                poolingBullet.GetComponent<Rigidbody2D>().velocity = playerPos * bullet.speed;

            });
        }
    }

    void FireShotToPlayer2(GameObject poolingBullet)
    {
        Vector2 playerPos = (player.transform.position - transform.position).normalized;
        
        poolingBullet.transform.position = transform.position + new Vector3(0, -1.4f, 0);
        poolingBullet.transform.rotation = Quaternion.Euler(0, 0, 0);

        var bullet = poolingBullet.GetComponent<Bullet>();
        bullet.nameBullet = "MobBulletD";
        bullet.damage = damage;

        Vector2 ranVec = new Vector2(Random.Range(-0.4f, 0.4f), Random.Range(-0.05f, -0.15f));
        playerPos += ranVec;
        poolingBullet.GetComponent<Rigidbody2D>().velocity = playerPos * bullet.speed;
    }

    void FireShotToPlayer()
    {
        if (player == null) return;

        for (int i = 0; i < 10; i++)
        {
            FireShotToPlayer2(ObjectManager.Instance.GetRangedObject("MobBulletD"));
        }

        Debug.Log("무차별 발사");
    }

    IEnumerator ShotCoroutine(int count, float delay, Action action)
    {
        for (int i = 0; i < count; i++)
        {
            action();
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator ShotCoroutine(int count, float delay, Action<int> action, int pram)
    {
        for (int i = 0; i < count; i++)
        {
            action(pram);
            yield return new WaitForSeconds(delay);
        }
    }

    void FireArc(int i)
    {
        ObjectManager.Instance.GetRangedObject("MobBulletD", (poolingBullet) =>
        {
            poolingBullet.transform.position = transform.position + new Vector3(0, -1.4f, 0);
            poolingBullet.transform.rotation = Quaternion.Euler(0, 0, 0);

            var bullet = poolingBullet.GetComponent<Bullet>();
            bullet.nameBullet = "MobBulletD";
            bullet.damage = damage;
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * i / 20), -1);
            poolingBullet.GetComponent<Rigidbody2D>().velocity = dirVec * bullet.speed;
        });

    }

    void FireHalfAround(int a)
    {
        int count = 15;
        int MAX = a % 2 == 0 ? count : count + 1;

        for (int i = 0; i <= MAX; i++)
        {
            ObjectManager.Instance.GetRangedObject("MobBulletD", (poolingBullet) =>
            {
                poolingBullet.transform.position = transform.position + new Vector3(0, -1.4f, 0);
                poolingBullet.transform.rotation = Quaternion.Euler(0, 0, 0);

                var bullet = poolingBullet.GetComponent<Bullet>();
                bullet.nameBullet = "MobBulletD";
                bullet.damage = damage;

                Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * i / MAX), -Mathf.Sin(Mathf.PI * i / MAX));
                Vector3 rotVec = new Vector3(0, 0, Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg + 90);
                bullet.transform.rotation = Quaternion.Euler(rotVec);

                poolingBullet.GetComponent<Rigidbody2D>().velocity = dirVec * bullet.speed / 2;
            });
        }
    }

    // 몹에 따른 총알 발사
    public void Fire()
    {
        if (player == null) return;

        if (mobName == "S")
        {
            ObjectManager.Instance.GetRangedObject("MobBulletA", (poolingBullet) =>
            {
                poolingBullet.transform.position = transform.position + new Vector3(0, -1.4f, 0);
                poolingBullet.transform.rotation = Quaternion.Euler(0, 0, 0);

                var bullet = poolingBullet.GetComponent<Bullet>();
                bullet.nameBullet = "MobBulletA";
                bullet.damage = damage;
                Vector2 playerPos = (player.transform.position - transform.position).normalized;
                poolingBullet.GetComponent<Rigidbody2D>().velocity = playerPos * bullet.speed;
            });
        }
        else
        {
            string bb = mobName == "M" ? "MobBulletA" : "MobBulletB";

            ObjectManager.Instance.GetRangedObject(bb, (poolingBullet) =>
            {
                poolingBullet.transform.position = transform.position + new Vector3(0, -1.4f, 0);
                poolingBullet.transform.rotation = Quaternion.Euler(0, 0, 0);

                var bullet = poolingBullet.GetComponent<Bullet>();
                bullet.nameBullet = bb;
                bullet.damage = damage;
                Vector2 playerPos = (player.transform.position - transform.position).normalized;
                poolingBullet.GetComponent<Rigidbody2D>().velocity = playerPos * bullet.speed;
            });
        }


    }

    public void MoveSide(Vector2 _vector)
    {
        if (_vector == Vector2.left) transform.rotation = Quaternion.Euler(0, 0, -45);
        else if (_vector == Vector2.right) transform.rotation = Quaternion.Euler(0, 0, 45);

        rb.velocity += _vector * speed;
    }

    // 총알에 맞았을 경우
    public new void OnHit(int _damage)
    {
        if (curHp <= 0) return;

        curHp -= _damage;
        if (mobName == "B")
            animator.SetTrigger("OnHit");

        spriteRenderer.sprite = sprite[1];
        Invoke("ReturnSprite", 0.1f);

        if (curHp <= 0)
        {
            GameManager.Instance.AddScore(score);

            if (mobName == "B")
            {
                CancelInvoke("BossRandomFire");
                GameManager.Instance.VictoryGame();
            }

            CancelInvoke("Fire");
            PushObject(gameObject);

            DropRandomItem();
        }
    }

    // 랜덤 아이템 드랍
    void DropRandomItem()
    {
        int ran = mobName == "B" ? -1 : Random.Range(0, 100);
        if (ran < 60) return;   //Debug.Log("아이템 없음");
        else if (ran < 90)      // Coin
        {
            ObjectManager.Instance.GetRangedObject("ItemCoin", (item) =>
            {
                item.transform.position = transform.position;
                item.transform.rotation = Quaternion.Euler(0, 0, 0);
            });
        }
        else if (ran < 95)      // Power
        {
            ObjectManager.Instance.GetRangedObject("ItemPower", (item) =>
            {
                item.transform.position = transform.position;
                item.transform.rotation = Quaternion.Euler(0, 0, 0);
            });
        }
        else if (ran < 100)     // Boom
        {
            ObjectManager.Instance.GetRangedObject("ItemBoom", (item) =>
            {
                item.transform.position = transform.position;
                item.transform.rotation = Quaternion.Euler(0, 0, 0);
            });
        }
    }

    // 원래 스프라이트로 변경
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprite[0];
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        // 경계선에 닿을 경우 (밖으로 나갈 경우)
        if (_collision.transform.CompareTag("Border"))
            PushObject(gameObject);

        // 유저가 보스와 충돌 했을 경우
        if (_collision.transform.CompareTag("Player") && transform.CompareTag("MobBoss"))
            return;

        // 유저와 충돌 했을 경우
        if (_collision.transform.CompareTag("Player"))
            PushObject(gameObject);
    }


    void PushObject(GameObject _gameObject)
    {
        // gameObject.name.Substring();
        string myName = _gameObject.name.Replace("(Clone)", "");
        // gameObject.name.Split('(')[0];

        ObjectManager.Instance.PushRangedObject(myName, _gameObject);
    }
}
