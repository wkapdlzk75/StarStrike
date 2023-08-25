using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int HP;                      // 체력
    public float speed;                 // 플레이어 속도
    public float bulletFiringInterval;  // 총알 발사 간격
    public float lastSpawnTime;                // 마지막 총알 발사 시각

    public GameObject bulletsParent;    // 총알의 관리 오브젝트 (부모)
    public Bullet bulletPrefabA;        // 총알 프리팹A
    public Bullet bulletPrefabB;        // 총알 프리팹B
    void OnHit(int _damage)
    {
        HP -= _damage;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

}
