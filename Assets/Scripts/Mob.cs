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
    Rigidbody2D rb;

    public Player player;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    private void Start()
    {
        // 최초 몹 소환 2초 후 총알 발사, bulletFiringInterval초 마다 총알 발사
        InvokeRepeating("Fire", 2, bulletFiringInterval);
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
        if (_vector == Vector2.left)    transform.rotation = Quaternion.Euler(0, 0, -45);
        else if (_vector == Vector2.right)  transform.rotation = Quaternion.Euler(0, 0, 45);

        rb.velocity += _vector * speed;
    }

    // 총알에 맞았을 경우
    public new void OnHit(int _damage)
    {
        if (HP <= 0) return;

        HP -= _damage;
        //Debug.Log("현재 체력 : " + HP);
        spriteRenderer.sprite = sprite[1];
        Invoke("ReturnSprite", 0.1f);

        if (HP <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.AddScore(score);

            // 랜덤 아이템 드랍
            int ran = Random.Range(0, 100);
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
