using System.Collections.Generic;
using UnityEngine;

public class Mob : Unit
{
    public string mobName;
    public int score;
    public Sprite[] sprite;

    public Item itemCoin;
    public Item itemPower;
    public Item itemBoom;

    SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rb;

    public Player player;
    Vector2 playerPos;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
        if (mobName == "B")
            animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // 최초 몹 소환 2초 후 총알 발사, bulletFiringInterval초 마다 총알 발사
        InvokeRepeating("Fire", 2, bulletFiringInterval);
    }

    // 몹에 따른 총알 발사
    public void Fire()
    {
        if (player == null) return;
        PlayerLocationTracking(player);

        Bullet b;

        if (mobName == "S")
            b = BulletManager.instance.Create(transform);
        else
            b = Instantiate(mobName == "M" ? bulletPrefabA : bulletPrefabB, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);

        if (b != null)
        {
            b.damage = damage;
            Rigidbody2D bb = b.GetComponent<Rigidbody2D>();
            Vector2 playerPos = (player.transform.position - transform.position).normalized;
            bb.velocity = playerPos * b.speed;
        }

    }

    public void PlayerLocationTracking(Player _player)
    {
        playerPos = (_player.transform.position - transform.position).normalized;
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
            GameManager.instance.AddScore(score);

            if (mobName == "B") GameManager.instance.VictoryGame();

            Destroy(gameObject);

            DropRandomItem();
        }
    }

    // 랜덤 아이템 드랍
    void DropRandomItem()
    {
        int ran = mobName == "B" ? -1 : Random.Range(0, 100);
        if (ran < 60) return;   //Debug.Log("아이템 없음");
        else if (ran < 90)      // Coin
            Instantiate(itemCoin, transform.position, itemCoin.transform.rotation);
        else if (ran < 95)      // Power
            Instantiate(itemPower, transform.position, itemPower.transform.rotation);
        else if (ran < 100)     // Boom
            Instantiate(itemBoom, transform.position, itemBoom.transform.rotation);
    }

    // 원래 스프라이트로 변경
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprite[0];
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        // 경계선에 닿을 경우 (밖으로 나갈 경우) 또는 유저와 충돌 했을 경우
        if (_collision.transform.CompareTag("Border")||
            _collision.transform.CompareTag("Player"))
            Destroy(gameObject);

        // 유저가 보스와 충돌 했을 경우
        if (_collision.transform.CompareTag("Player") && transform.CompareTag("MobBoss"))
            return;
    }

    void FireFowardToPlayerCoroutine2()
    {
        // 리스트나 배열로 해서 중복 제거
        if (player == null) return;
        PlayerLocationTracking(player);
        float angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;

        // 파일 path 방식
        // 단점
        // 폴더 관리가 까다롭다.
        // 파일명이 오타가 날 경우 애러가 난다.

        // 멤버 변수 참조 방식
        // 단점 : 오브젝트를 불러올 경우, 스크립트에 멤버변수를 선언하고, 프리팹을 넣어 두어야 함.

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

                // GetComponent 는 성능 비용이 큼
                var bullet = poolingBullet.GetComponent<Bullet>();
                bullet.nameBullet = "MobBulletC";
                bullet.damage = damage;

                poolingBullet.GetComponent<Rigidbody2D>().velocity = playerPos * bullet.speed;

            });
        }
    }

}
