using System;
using System.Collections.Generic;
using UnityEngine;

public class Follower : Unit
{
    public Vector3 followPos;
    public int followDelay;
    int index;

    GameObject playerObject;
    Transform parent;
    Vector3[] vector3Pos;
    Queue<Vector3> parentPos = new Queue<Vector3>();

    void Start()
    {
        vector3Pos = new Vector3[4];
        CreateArray();
    }
    public void Create(Player player, int num)
    {
        playerObject = player.gameObject;
        parent = player.transform;
        index = num;
    }
    void Update()
    {
        Watch();
        Follow();
        //Fire();
    }

    void CreateArray()
    {
        vector3Pos[0] = new Vector3(-0.65f, -0.65f, 0);
        vector3Pos[1] = new Vector3(0.65f, -0.65f, 0);
        vector3Pos[2] = new Vector3(-1.3f, -1f, 0);
        vector3Pos[3] = new Vector3(1.3f, -1f, 0);
    }

    // 플레이어 위치 추적
    void Watch()
    {
        try
        {
            if (!playerObject.activeSelf)
                Destroy(gameObject);
        }
        catch (Exception)
        {
            Destroy(gameObject);
            return;
        }

        if (!parentPos.Contains(parent.position))
            parentPos.Enqueue(parent.position + vector3Pos[index]);

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
        //Instantiate(bulletPrefabA, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
    }
}
