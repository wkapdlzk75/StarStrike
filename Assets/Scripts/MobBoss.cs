using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBoss : Mob
{
    Rigidbody2D rb;

    Vector2 playerPos;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    private void Start()
    {
        if (mobName == "B")
        {
            Invoke("MoveStop", 3);
            return;
        }
        // 최초 몹 소환 2초 후 총알 발사, bulletFiringInterval초 마다 총알 발사
        InvokeRepeating("Fire", 2, bulletFiringInterval);
    }

    void MoveStop()
    {
        //if (!gameObject.activeSelf)
        //    return;
        rb.velocity = Vector2.zero;
        InvokeRepeating("BossRandomFire", 4, 6);
    }

    void BossRandomFire()
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

    void FireShotToPlayer()
    {
        if (player == null) return;
        Vector2 playerPos = (player.transform.position - transform.position).normalized;
        for (int i = 0; i < 10; i++)
        {
            Bullet bb = Instantiate(bulletPrefabB, transform.position + new Vector3(0, -1.4f, 0), Quaternion.Euler(0, 0, 0));
            bb.damage = damage;
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
        bb.damage = damage;
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
            bb.damage = damage;
            Rigidbody2D rb = bb.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * i / MAX), -Mathf.Sin(Mathf.PI * i / MAX));
            Vector3 rotVec = new Vector3(0, 0, Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg + 90);
            bb.transform.rotation = Quaternion.Euler(rotVec);
            rb.velocity = dirVec * bb.speed / 2;

        }

    }
}
