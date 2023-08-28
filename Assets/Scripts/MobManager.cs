using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class MobManager : Manager
{
    public GameObject parent;
    public Mob[] mobPrefab;
    int stage;                          // stage 정보
    int repeatCount;                    // 적 스폰 반복 횟수
    public Transform[] spawnPoints;     // 적 스폰 위치
    public float spawnInterval;         // 적 스폰 시간 간격

    public Player player;               // 플레이어

    private void Start()
    {

        try
        {
            stage = LobbyManager.instance.stageInt;
        }
        catch (NullReferenceException e)
        {
            stage = 1;
            //Debug.LogError("Player reference is null: " + e.Message);
        }
        repeatCount = 0;
        Invoke("GameStart", 3);    // 게임 시작후 3초 뒤 몹 생성
        //InvokeRepeating("Create", 5, 5);
    }

    // 게임 시작
    public void GameStart()
    {
        StartCoroutine(RepeatSpawnMob());
    }

    IEnumerator RepeatSpawnMob()
    {
        if (stage < 4) // 1~3 stage
        {
            // stage 에 따른 랜덤 몹 생성
            while (repeatCount < 5 * stage)
            {
                int rangeMob = UnityEngine.Random.Range(0, stage);
                SpawnMob(rangeMob);
                repeatCount++;
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        else
        {
            while (repeatCount < 15)
            {
                int rangeMob = UnityEngine.Random.Range(0, 3);
                SpawnMob(rangeMob);
                SideSpawnMob(rangeMob); // 50%의 확률로 스폰
                repeatCount++;
                yield return new WaitForSeconds(spawnInterval);
            }
        }

    }

    // 동시에 같은 몹 5마리 생성
    public void SpawnMob(int _a)
    {
        for (int i = 0; i < 5; i++)
        {
            Mob mob = Instantiate(mobPrefab[_a], spawnPoints[i].position, spawnPoints[i].rotation, parent.transform);
            mob.player = player;
        }
    }

    public void SideSpawnMob(int _a)
    {
        int probability = UnityEngine.Random.Range(0, 2);
        if (probability == 0)
        {
            int rangeMob = UnityEngine.Random.Range(5, 9);
            Mob mob = Instantiate(mobPrefab[_a], spawnPoints[rangeMob].position, spawnPoints[rangeMob].rotation, parent.transform);
            mob.player = player;
            if (rangeMob == 5 || rangeMob == 6)         // 5, 6은 오른쪽 사이드 스폰   
            {
                mob.MoveSide(Vector2.left);   // 왼쪽으로 이동
            }
            else if (rangeMob == 7 || rangeMob == 8)    // 7, 8은 왼쪽 사이드 스폰
            {
                mob.MoveSide(Vector2.right);    // 오른쪽으로 이동
            }

        }

    }

}
