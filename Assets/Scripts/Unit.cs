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
    void Start()
    {
        //string str = CSVManager.Instance.GetItemString(1, "Name");
        //int a = CSVManager.Instance.GetItemInt(1,"MaxHP");
        //Debug.Log(str);
        //Debug.Log(a);
    }

    public void ActiveExplosion(string type)
    {
        ObjectManager.Instance.GetRangedObject("Explosion", (explosion) =>
        {
            Explosion ex = explosion.GetComponent<Explosion>();
            ex.transform.position = transform.position;
            ex.transform.rotation = Quaternion.Euler(0, 0, 0);
            ex.StartExplosion(type);
        });
    }

}
