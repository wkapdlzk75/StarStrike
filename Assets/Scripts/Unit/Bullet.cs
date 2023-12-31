using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed; // 총알 스피드
    public int damage;  // 공격력
    public float scale = 1f;
    

    public string nameBullet;
    Rigidbody2D rb;
    GameObject playerObject;

    private void Start()
    {
        
    }

    void OnEnable()
    {
        transform.localScale = Vector3.one * scale;
        Invoke("AutoPush", 10);
        

        // 플레이어 총알의 경우
        if (transform.CompareTag("PlayerBullet"))
        {
            rb = GetComponent<Rigidbody2D>();
            playerObject = GameObject.FindWithTag("Player");
            damage = playerObject.GetComponent<Player>().damage;
            rb.velocity = Vector2.up * speed;
        }

        // 팔로워 총알의 경우
        if (transform.CompareTag("FollowerBullet"))
        {
            rb = GetComponent<Rigidbody2D>();
            damage = 5;
            speed = 5;
            rb.velocity = Vector2.up * speed;
        }
    }

    private void OnDisable()
    {
        CancelInvoke("AutoPush");
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        // 경계선에 닿을 경우 (밖으로 나갈 경우)
        if (_collision.transform.CompareTag("Border"))
            PushBullet();

        // 적이 맞았을 경우
        if ((transform.CompareTag("FollowerBullet") || transform.CompareTag("PlayerBullet")) &&
            (_collision.transform.CompareTag("Mob") || _collision.transform.CompareTag("MobBoss")))
        {
            PushBullet();
            _collision.gameObject.GetComponent<Mob>().OnHit(damage);
        }

        // 플레이어가 맞았을 경우
        if (transform.CompareTag("MobBullet") && _collision.transform.CompareTag("Player"))
            _collision.gameObject.GetComponent<Player>().OnHit(damage);

    }



    void AutoPush()
    {
        if (gameObject.activeSelf)
        {
            PushBullet();
        }
    }

    void PushBullet()
    {
        if (!gameObject.activeSelf) { return; }

        // gameObject.name.Substring();
        string myName = gameObject.name.Replace("(Clone)", "");
        // gameObject.name.Split('(')[0];

        ObjectManager.Instance.PushRangedObject(myName, gameObject);
    }

}
