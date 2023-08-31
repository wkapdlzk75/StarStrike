using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;
    public Bullet bulletPrefabA;
    public GameObject mobobject;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public Bullet Create(Transform tParent)
    {
        Bullet b = Instantiate(bulletPrefabA, tParent.position + new Vector3(0, -0.5f, 0), tParent.rotation);//, bulletsParent.transform);
        b.transform.SetParent(mobobject.transform);
        return b;
    }

}
