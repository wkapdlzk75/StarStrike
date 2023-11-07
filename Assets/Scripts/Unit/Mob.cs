using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mob : Unit
{
    private const float SpriteReturnDelay = 0.1f;
    private const int RandomItemThreshold = 100;

    private const float BossMoveDuration = 3;
    private const float BossFireStartDelay = 4;
    private const float BossFireInterval = 7;

    public string mobName;
    public int score;
    private bool initEnd = false;

    public Sprite[] sprite;
    public AudioClip dieSound;
    public AudioClip shootingSound;
    public Action deathAction;
    public Player player;

    SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rb;

    private void OnEnable()
    {
        InitializeComponents();
        InitializeMob();

        if (mobName == "B")
        {
            InitializeBoss();
            return;
        }

        StartCoroutine(Fire());
    }

    private void OnDisable()
    {
        if (deathAction != null)
            deathAction();
    }

    void InitializeComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    void InitializeBoss()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(MoveStop());
    }

    public void InitializeMob()
    {
        curHp = maxHp;
        initEnd = true;
    }

    IEnumerator MoveStop()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(BossMoveDuration);
        StartCoroutine(BossFire());
    }

    IEnumerator BossFire()
    {
        yield return new WaitForSeconds(BossMoveDuration);

        while (true)
        {
            ChooseRandomBossAttack();
            yield return new WaitForSeconds(BossFireInterval);
        }
    }

    void ChooseRandomBossAttack()
    {
        int ran = Random.Range(0, 4);
        switch (ran)
        {
            case 0:
                StartCoroutine(ExecuteRepeatedAttack(10, 0.15f, FireBulletsTowardsPlayer));
                break;
            case 1:
                StartCoroutine(ExecuteRepeatedAttack(1, 0, FireRandomShotsAtPlayer));
                break;
            case 2:
                StartCoroutine(ExecuteRepeatedAttack(45, 0.1f, FireBulletsInArc));
                break;
            case 3:
                StartCoroutine(ExecuteRepeatedAttack(5, 0.75f, FireBulletsInHalfCircle));
                break;
        }
    }

    IEnumerator ExecuteRepeatedAttack(int count, float delay, Action action)
    {
        for (int i = 0; i < count; i++)
        {
            action();
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator ExecuteRepeatedAttack(int count, float delay, Action<int> action)
    {
        for (int i = 0; i < count; i++)
        {
            action(i);
            yield return new WaitForSeconds(delay);
        }
    }

    void FireBulletsTowardsPlayer()
    {
        GameManager.Instance.PlaySound(shootingSound, GameManager.Instance.masterVolume * 0.25f);

        // 리스트나 배열로 해서 중복 제거
        if (player == null) return;
        Vector2 playerPos = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;

        List<Vector3> vectors = new List<Vector3>();
        vectors.Add(new Vector3(0.58f, -1.4f, 0));
        vectors.Add(new Vector3(0.87f, -1.4f, 0));
        vectors.Add(new Vector3(-0.58f, -1.4f, 0));
        vectors.Add(new Vector3(-0.87f, -1.4f, 0));


        // 4발씩 10번
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

    void FireRandomShotsAtPlayer2(GameObject poolingBullet)
    {
        // 리스트나 배열로 해서 중복 제거
        if (player == null) return;
        Vector2 playerPos = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;

        poolingBullet.transform.position = transform.position + new Vector3(0, -1.4f, 0);
        poolingBullet.transform.rotation = Quaternion.Euler(0, 0, angle - 270);

        var bullet = poolingBullet.GetComponent<Bullet>();
        bullet.nameBullet = "MobBulletD";
        bullet.damage = damage;

        Vector2 ranVec = new Vector2(Random.Range(-0.4f, 0.4f), Random.Range(-0.05f, -0.15f));
        playerPos += ranVec;
        poolingBullet.GetComponent<Rigidbody2D>().velocity = playerPos * bullet.speed;
    }

    void FireRandomShotsAtPlayer()
    {
        if (player == null) return;
        GameManager.Instance.PlaySound(shootingSound, GameManager.Instance.masterVolume * 0.25f);

        for (int i = 0; i < 10; i++)
        {
            FireRandomShotsAtPlayer2(ObjectManager.Instance.GetRangedObject("MobBulletD"));
        }
    }

    void FireBulletsInArc(int i)
    {
        GameManager.Instance.PlaySound(shootingSound, GameManager.Instance.masterVolume * 0.25f);
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

    void FireBulletsInHalfCircle(int a)
    {
        GameManager.Instance.PlaySound(shootingSound, GameManager.Instance.masterVolume * 0.25f);
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
    IEnumerator Fire()
    {
        yield return new WaitUntil(() => initEnd == true);

        if (player == null || !player.gameObject.activeSelf)
        {
            yield break;
        }

        while (gameObject.activeSelf)
        {
            if (mobName == "S")
            {
                ObjectManager.Instance.GetRangedObject("MobBulletA", (poolingBullet) =>
                {
                    poolingBullet.transform.position = transform.position + new Vector3(0, -0.4f, 0);
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
                    poolingBullet.transform.position = transform.position + new Vector3(0, -0.7f, 0);
                    poolingBullet.transform.rotation = Quaternion.Euler(0, 0, 0);

                    var bullet = poolingBullet.GetComponent<Bullet>();
                    bullet.nameBullet = bb;
                    bullet.damage = damage;
                    Vector2 playerPos = (player.transform.position - transform.position).normalized;
                    poolingBullet.GetComponent<Rigidbody2D>().velocity = playerPos * bullet.speed;
                });
            }

            yield return new WaitForSeconds(bulletFiringInterval);
        }
    }

    public void MoveSide(Vector2 _vector)
    {
        if (_vector == Vector2.left) transform.rotation = Quaternion.Euler(0, 0, -45);
        else if (_vector == Vector2.right) transform.rotation = Quaternion.Euler(0, 0, 45);

        rb.velocity += _vector * speed;
    }

    // 총알에 맞았을 경우
    public void OnHit(int _damage)
    {
        if (curHp <= 0) return;

        curHp -= _damage;
        if (mobName == "B")
            animator.SetTrigger("OnHit");

        spriteRenderer.sprite = sprite[1];
        Invoke("ReturnSprite", 0.1f);

        if (curHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.Instance.AddScore(score);
        ActiveExplosion(mobName);

        if (!player.isBoomActive)
            GameManager.Instance.PlaySound(dieSound, GameManager.Instance.masterVolume * 0.35f);

        if (mobName == "B")
        {
            CancelInvoke("BossRandomFire");
            GameManager.Instance.EndGame(true);
        }

        CancelInvoke("Fire");
        PushObject(gameObject);

        DropRandomItem();
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
        CancelInvoke("ReturnSprite");
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
        {
            if (player.isRespawnTime) return;
            PushObject(gameObject);
        }

    }


    void PushObject(GameObject _gameObject)
    {
        GameManager.Instance.spawnCount--;
        // gameObject.name.Substring();
        string myName = _gameObject.name.Replace("(Clone)", "");
        // gameObject.name.Split('(')[0];

        ObjectManager.Instance.PushRangedObject(myName, _gameObject);
    }
}
