using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public GameObject parent;
    public Mob[] mobPrefab;
    int stage;
    private void Start()
    {
        stage = RobbyManager.instance.stageInt;
        InvokeRepeating("Create", 5, 5);
    }
    public void Create()
    {
        for (int i = -2; i < 3; i++)
        {
            Mob mob = Instantiate(mobPrefab[0]);
            mob.transform.SetParent(parent.transform);
            mob.transform.localPosition = new Vector2(i, 0);

        }

    }
}
