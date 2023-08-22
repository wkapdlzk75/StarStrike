using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Unit
{
    public Sprite[] sprite;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
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
