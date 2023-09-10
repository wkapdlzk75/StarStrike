using System.Runtime.InteropServices.WindowsRuntime;
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
        if (mobName == "B")
        {
            Invoke("moveStop", 5);
            return;
        }
        // 최초 몹 소환 2초 후 총알 발사, bulletFiringInterval초 마다 총알 발사
        InvokeRepeating("Fire", 2, bulletFiringInterval);
    }

    void moveStop()
    {
        //if (!gameObject.activeSelf)
        //    return;
        rb.velocity = Vector2.zero;
        InvokeRepeating("bossRandomFire", 5,5);
    }

    void bossRandomFire()
    {
        int ran = Random.Range(0, 1);
        switch (ran)
        {
            case 0:
                FireFowardToPlayer();
                break;
            case 1:
                FireShotgunToPlayer();
                break;
            case 2:
                FireShotgun();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    void FireFowardToPlayer()
    {
        Bullet br = Instantiate(bulletPrefabA, transform.position + new Vector3(0.58f, -1.4f, 0), transform.rotation);
        Bullet brr = Instantiate(bulletPrefabA, transform.position + new Vector3(0.87f, -1.4f, 0), transform.rotation);
        Bullet bl = Instantiate(bulletPrefabA, transform.position + new Vector3(-0.58f, -1.4f, 0), transform.rotation);
        Bullet bll = Instantiate(bulletPrefabA, transform.position + new Vector3(-0.87f, -1.4f, 0), transform.rotation);

        br.Damage = gameObject.GetComponent<Mob>().Damage;
        brr.Damage = gameObject.GetComponent<Mob>().Damage;
        bl.Damage = gameObject.GetComponent<Mob>().Damage;
        bll.Damage = gameObject.GetComponent<Mob>().Damage;

        Rigidbody2D bbr = br.GetComponent<Rigidbody2D>();
        Rigidbody2D bbrr = brr.GetComponent<Rigidbody2D>();
        Rigidbody2D bbl = bl.GetComponent<Rigidbody2D>();
        Rigidbody2D bbll = bll.GetComponent<Rigidbody2D>();

        Vector2 playerPos = (player.transform.position - transform.position).normalized;

        bbr.velocity = playerPos * br.speed;
        bbrr.velocity = playerPos * brr.speed;
        bbl.velocity = playerPos * bl.speed;
        bbll.velocity = playerPos * bll.speed;

        Debug.Log("4발 발사");
    }
    void FireShotgunToPlayer()
    {
        Debug.Log("샷건2 발사");
    }

    void FireShotgun()
    {
        Debug.Log("샷건 발사");
    }

    void FireAround()
    {
        Debug.Log("원형 발사");
    }

    // 몹에 따른 총알 발사
    public void Fire()
    {
        if (player != null)
            if (mobName == "S")
            {

                Bullet b = BulletManager.instance.Create(transform);
                b.Damage = gameObject.GetComponent<Mob>().Damage;
                //Bullet b = Instantiate(bulletPrefabA, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);//, bulletsParent.transform);
                Rigidbody2D bb = b.GetComponent<Rigidbody2D>();
                Vector2 playerPos = (player.transform.position - transform.position).normalized;
                bb.velocity = playerPos * b.speed;
                //Debug.Log(playerPos + " " + b.speed + " " + bb.velocity + "S");
            }
            else if (mobName == "M")
            {
                Bullet b = Instantiate(bulletPrefabA, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);//, bulletsParent.transform);
                b.Damage = gameObject.GetComponent<Mob>().Damage;
                Rigidbody2D bb = b.GetComponent<Rigidbody2D>();
                Vector2 playerPos = (player.transform.position - transform.position).normalized;
                bb.velocity = playerPos * b.speed;
                //Debug.Log(playerPos + " " + b.speed + " " + bb.velocity + "M");
            }
            else if (mobName == "L")
            {
                Bullet b = Instantiate(bulletPrefabB, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);//, bulletsParent.transform);
                b.Damage = gameObject.GetComponent<Mob>().Damage;
                Rigidbody2D bb = b.GetComponent<Rigidbody2D>();
                Vector2 playerPos = (player.transform.position - transform.position).normalized;
                bb.velocity = playerPos * b.speed;
                //Debug.Log(playerPos + " " + b.speed + " " + bb.velocity + "L");
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
        if (HP <= 0) return;

        HP -= _damage;
        if (mobName == "B")
            animator.SetTrigger("OnHit");
        //Debug.Log("현재 체력 : " + HP);
        spriteRenderer.sprite = sprite[1];
        Invoke("ReturnSprite", 0.1f);

        if (HP <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.AddScore(score);

            // 랜덤 아이템 드랍
            int ran = mobName == "B" ? 0 : Random.Range(0, 100);
            if (ran < 60)
                return;//Debug.Log("아이템 없음");
            else if (ran < 90)  // Coin
                Instantiate(itemCoin, transform.position, itemCoin.transform.rotation);
            else if (ran < 95)  // Power
                Instantiate(itemPower, transform.position, itemPower.transform.rotation);
            else if (ran < 100) // Boom
                Instantiate(itemBoom, transform.position, itemBoom.transform.rotation);
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
            Destroy(gameObject);

        // 유저와 충돌 했을 경우
        if (_collision.transform.CompareTag("Player"))
            Destroy(gameObject);
    }
}
