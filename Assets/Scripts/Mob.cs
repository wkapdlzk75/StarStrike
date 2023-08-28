using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Unit
{
    public string mobName;
    public int score;
    public Sprite[] sprite;

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
        {
            if (mobName == "S")
            {

                Bullet b = BulletManager.instance.Create(transform);

                //Bullet b = Instantiate(bulletPrefabA, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);//, bulletsParent.transform);
                Rigidbody2D bb = b.GetComponent<Rigidbody2D>();
                Vector2 playerPos = (player.transform.position - transform.position).normalized;
                bb.velocity = playerPos * b.speed;
                //Debug.Log(playerPos + " " + b.speed + " " + bb.velocity + "S");
            }
            else if (mobName == "M")
            {
                Bullet b = Instantiate(bulletPrefabA, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);//, bulletsParent.transform);
                Rigidbody2D bb = b.GetComponent<Rigidbody2D>();
                Vector2 playerPos = (player.transform.position - transform.position).normalized;
                bb.velocity = playerPos * b.speed;
                //Debug.Log(playerPos + " " + b.speed + " " + bb.velocity + "M");
            }
            else if (mobName == "L")
            {
                Bullet b = Instantiate(bulletPrefabB, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);//, bulletsParent.transform);
                Rigidbody2D bb = b.GetComponent<Rigidbody2D>();
                Vector2 playerPos = (player.transform.position - transform.position).normalized;
                bb.velocity = playerPos * b.speed;
                //Debug.Log(playerPos + " " + b.speed + " " + bb.velocity + "L");
            }
        }
        

    }


    public void MoveSide(Vector2 _vector)
    {
        if (_vector == Vector2.left)
            transform.rotation = Quaternion.Euler(0, 0, -45);
        else if (_vector == Vector2.right)
            transform.rotation = Quaternion.Euler(0, 0, 45);

        rb.velocity += _vector * speed;
    }

    // 총알에 맞았을 경우
    public void OnHit(int _damage)
    {
        HP -= _damage;
        spriteRenderer.sprite = sprite[1];
        Invoke("ReturnSprite", 0.1f);

        if (HP <= 0)
        {
            Destroy(gameObject);
            //Addscore(score);
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
        {
            Destroy(gameObject);
        }

        // 유저와 충돌 했을 경우
        if (_collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
