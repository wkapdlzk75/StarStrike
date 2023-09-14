using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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
            Invoke("moveStop", 3);
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
        InvokeRepeating("bossRandomFire", 4, 6);
    }

    void bossRandomFire()
    {
        int ran = Random.Range(0, 4);
        switch (ran)
        {
            case 0:
                FireFowardToPlayer();
                break;
            case 1:
                FireShotToPlayer();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireHalfAround();
                break;
        }
    }
    void FireFowardToPlayer()
    {
        StartCoroutine(FireFowardToPlayerCoroutine1());
        Debug.Log("4발씩 발사");
    }

    IEnumerator FireFowardToPlayerCoroutine1()
    {
        for (int i = 0; i < 7; i++)
        {
            FireFowardToPlayerCoroutine2();
            yield return new WaitForSeconds(0.1f);
        }
    }

    void FireFowardToPlayerCoroutine2()
    {
        // 리스트나 배열로 해서 중복 제거
        if (player == null) return;
        Vector2 playerPos = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;

        // 좌우 대칭 함수
        Bullet br = Instantiate(bulletPrefabA, transform.position + new Vector3(0.58f, -1.4f, 0), Quaternion.Euler(0, 0, angle - 270));
        Bullet brr = Instantiate(bulletPrefabA, transform.position + new Vector3(0.87f, -1.4f, 0), Quaternion.Euler(0, 0, angle - 270));
        Bullet bl = Instantiate(bulletPrefabA, transform.position + new Vector3(-0.58f, -1.4f, 0), Quaternion.Euler(0, 0, angle - 270));
        Bullet bll = Instantiate(bulletPrefabA, transform.position + new Vector3(-0.87f, -1.4f, 0), Quaternion.Euler(0, 0, angle - 270));

        br.Damage = gameObject.GetComponent<Mob>().Damage;
        brr.Damage = gameObject.GetComponent<Mob>().Damage;
        bl.Damage = gameObject.GetComponent<Mob>().Damage;
        bll.Damage = gameObject.GetComponent<Mob>().Damage;

        Rigidbody2D bbr = br.GetComponent<Rigidbody2D>();
        Rigidbody2D bbrr = brr.GetComponent<Rigidbody2D>();
        Rigidbody2D bbl = bl.GetComponent<Rigidbody2D>();
        Rigidbody2D bbll = bll.GetComponent<Rigidbody2D>();

        bbr.velocity = playerPos * br.speed;
        bbrr.velocity = playerPos * brr.speed;
        bbl.velocity = playerPos * bl.speed;
        bbll.velocity = playerPos * bll.speed;


    }
    void FireShotToPlayer()
    {
        if (player == null) return;
        Vector2 playerPos = (player.transform.position - transform.position).normalized;
        for (int i = 0; i < 10; i++)
        {
            Bullet bb = Instantiate(bulletPrefabB, transform.position + new Vector3(0, -1.4f, 0), Quaternion.Euler(0, 0, 0));
            bb.Damage = gameObject.GetComponent<Mob>().Damage;
            Rigidbody2D rb = bb.GetComponent<Rigidbody2D>();
            Vector2 ranVec = new Vector2(Random.Range(-0.4f, 0.4f), Random.Range(-0.05f, -0.15f));
            playerPos += ranVec;
            rb.velocity = playerPos * bb.speed;
        }

        Debug.Log("무차별 발사");
    }


    void FireArc()
    {
        StartCoroutine(FireArcCoroutine1());
        Debug.Log("부채꼴 발사");
    }

    IEnumerator FireArcCoroutine1()
    {
        for (int i = 0; i < 51; i++)
        {
            FireArcCoroutine2(i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void FireArcCoroutine2(int i)
    {
        Bullet bb = Instantiate(bulletPrefabB, transform.position + new Vector3(0, -1.4f, 0), Quaternion.Euler(0, 0, 0));
        bb.Damage = gameObject.GetComponent<Mob>().Damage;
        Rigidbody2D rb = bb.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * i / 20), -1);

        rb.velocity = dirVec * bb.speed;
    }

    void FireHalfAround()
    {
        StartCoroutine(FireHalfAroundCoroutine1());
        Debug.Log("원형 발사");
    }

    IEnumerator FireHalfAroundCoroutine1()
    {
        for (int i = 0; i < 5; i++)
        {
            FireHalfAroundCoroutine2(i);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void FireHalfAroundCoroutine2(int a)
    {
        int count = 15;
        int MAX = a % 2 == 0 ? count : count + 1;
        for (int i = 0; i <= MAX; i++)
        {
            Bullet bb = Instantiate(bulletPrefabB, transform.position + new Vector3(0, -1.4f, 0), Quaternion.Euler(0, 0, 0));
            bb.Damage = gameObject.GetComponent<Mob>().Damage;
            Rigidbody2D rb = bb.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * i / MAX), -Mathf.Sin(Mathf.PI * i / MAX));
            Vector3 rotVec = new Vector3(0, 0, Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg + 90);
            bb.transform.rotation = Quaternion.Euler(rotVec);
            rb.velocity = dirVec * bb.speed / 2;

        }

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
            GameManager.instance.AddScore(score);
            if (mobName == "B")
                GameManager.instance.VictoryGame();
            Destroy(gameObject);

            // 랜덤 아이템 드랍
            int ran = mobName == "B" ? -1 : Random.Range(0, 100);
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
        if (_collision.transform.CompareTag("Player") && transform.CompareTag("MobBoss"))
            return;

        // 유저와 충돌 했을 경우
        if (_collision.transform.CompareTag("Player"))
            Destroy(gameObject);
    }
}
