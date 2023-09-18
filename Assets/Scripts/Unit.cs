using UnityEngine;

// 몹, 플레이어를 하위 클래스로 둔 상위 클래스
// 몹과 플레이어가 공통으로 가지고 있는 변수, 메소드

public class Unit : MonoBehaviour
{
    public int maxHp;                  // 최대 체력
    public int curHp;                      // 현재 체력
    public int damage;                  // 공격력
    public float speed;                 // 이동 속도
    public float bulletFiringInterval;  // 총알 발사 간격

    public GameObject bulletsParent;    // 총알의 관리 오브젝트 (부모)
    public Bullet bulletPrefabA;        // 총알 프리팹A
    public Bullet bulletPrefabB;        // 총알 프리팹B
    public void OnHit(int _damage)
    {
        curHp -= _damage;
    }

}
