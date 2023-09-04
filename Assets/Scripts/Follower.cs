using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Follower : Unit
{
    float lastFireTime;

    public Vector3 followPos;
    public int followeDelay;
    
    Transform parent;
    Queue<Vector3> parentPos = new Queue<Vector3>();

    void Start()
    {
        lastFireTime = 0;
    }
    public void Create(Player player)
    {
        parent = player.transform;
    }
    void Update()
    {
        Watch();
        Follow();
        Fire();
    }

    void Watch()
    {
        // Queue = FIFO
        parentPos.Enqueue(parent.position);
        if (parentPos.Count > followeDelay )
            followPos = parentPos.Dequeue();
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
