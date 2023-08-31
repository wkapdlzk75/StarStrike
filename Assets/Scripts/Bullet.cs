using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int Damage;  // 공격력

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // 플레이어의 총알의 경우
        if (transform.CompareTag("PlayerBullet"))
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            Player player = playerObject.GetComponent<Player>();
            Damage = player.Damage;
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
            //Debug.Log(gameObject.name + " 를 " + Damage + " 만큼 입힘.");
        }

        // 플레이어가 맞았을 경우
        if (transform.CompareTag("MobBullet") && _collision.transform.CompareTag("Player"))
            _collision.gameObject.GetComponent<Player>().OnHit(Damage);
            //Debug.Log(Damage + " 만큼 플레이어가 데미지 입음");

    }

}
