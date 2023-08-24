using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int HP;                      // 체력
    public float speed;                 // 플레이어 속도
    public float bulletFiringInterval;  // 총알 발사 간격

    void OnHit(int _damage)
    {
        HP -= _damage;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

}
