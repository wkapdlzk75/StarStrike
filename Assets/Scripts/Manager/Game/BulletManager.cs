using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;
    public Bullet bulletPrefabA;

    public GameObject boomEffect;
    public GameObject followerBullets;
    public GameObject mobBullets;
    public GameObject playerBullets;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    /*public Bullet Create(Transform tParent)
    {
        Bullet b = Instantiate(bulletPrefabA, tParent.position + new Vector3(0, -0.5f, 0), tParent.rotation);
        b.transform.SetParent(mobBullets.transform);
        return b;
    }*/

}
