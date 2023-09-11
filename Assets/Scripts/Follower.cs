using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Follower : Unit
{
    float lastFireTime;

    public Vector3 followPos;
    public int followDelay;

    GameObject playerObject;
    Transform parent;
    Queue<Vector3> parentPos = new Queue<Vector3>();

    void Start()
    {
        lastFireTime = 0;
    }
    public void Create(Player player)
    {
        playerObject = player.gameObject;
        parent = player.transform;
    }
    void Update()
    {
        Watch();
        Follow();
        Fire();
    }

    // 플레이어 위치 추적
    void Watch()
    {
        try {
            if (!playerObject.activeSelf)
                Destroy(gameObject);
        } catch (Exception NullReferenceException)
        {
            Destroy(gameObject);
            return;
        }

        if (!parentPos.Contains(parent.position))
            parentPos.Enqueue(parent.position + new Vector3(-0.75f, -0.75f, 0));

        // Queue = FIFO
        if (parentPos.Count > followDelay)
            followPos = parentPos.Dequeue();
        else if (parentPos.Count < followDelay)
            followPos = parent.position;

    }

    // 플레이어 따라가기
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
