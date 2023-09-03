using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Follower : Unit
{
    float lastFireTime;

    public Vector3 followPos;
    public int followeDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;

    void Start()
    {
        lastFireTime = 0;
    }

    void Update()
    {
        Watch();
        Follow();
        Fire();
    }

    void Watch()
    {
        Debug.Log(parent.position);
        // Queue = FIFO
        parentPos.Enqueue(parent.position);
        Debug.Log(parentPos);
        if (parentPos.Count > followeDelay )
            followPos = parentPos.Dequeue();
        Debug.Log(followPos);
    }

    void Follow()
    {
        transform.position = followPos;
    }

    // 총알 발사 *****
    public void Fire()
    {
        // bulletFiringInterval초 마다 총알 생성

        if (Time.time - lastFireTime > bulletFiringInterval)
        {
            Instantiate(bulletPrefabA, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            lastFireTime = Time.time;
        }
    }
}
