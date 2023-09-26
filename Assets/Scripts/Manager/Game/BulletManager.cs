using UnityEngine;

public class BulletManager : SSSingleton<BulletManager>
{
    public Bullet bulletPrefabA;

    public GameObject boomEffect;
    public GameObject followerBullets;
    public GameObject mobBullets;
    public GameObject playerBullets;

    /*public Bullet Create(Transform tParent)
    {
        Bullet b = Instantiate(bulletPrefabA, tParent.position + new Vector3(0, -0.5f, 0), tParent.rotation);
        b.transform.SetParent(mobBullets.transform);
        return b;
    }*/

}
