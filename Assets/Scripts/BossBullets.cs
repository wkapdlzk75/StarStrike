using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullets : Bullet
{
    public bool isRotate;

    void Update()
    {
        if (isRotate)
            transform.Rotate(Vector3.forward * 10);
    }
}
