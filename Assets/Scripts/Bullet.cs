using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int Damage;                  // 공격력

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (transform.CompareTag("PlayerBullet"))
        {
            rb.velocity = Vector2.up * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        // 경계선에 닿을 경우 (밖으로 나갈 경우)
        if (_collision.transform.CompareTag("Border"))
            Destroy(gameObject);

        // 적이 맞았을 경우
        if (transform.CompareTag("PlayerBullet") && _collision.transform.CompareTag("Mob"))
        {
            Destroy(gameObject);
            _collision.gameObject.GetComponent<Mob>().OnHit(Damage);
        }

    }

}
