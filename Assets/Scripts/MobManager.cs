using System.Collections;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public GameObject parent;
    public Mob[] mobPrefab;
    int stage;                          // stage 정보
    int repeatCount;                    // 적 스폰 반복 횟수
    public Transform[] spawnPoints;     // 적 스폰 위치
    public float spawnInterval;         // 적 스폰 시간 간격

    private void Start()
    {
        stage = RobbyManager.instance.stageInt;
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
        // stage 에 따른 랜덤 몹 생성
        while (repeatCount < 5 * stage)
        {
            int rangeMob = Random.Range(0, stage);
            SpawnMob(rangeMob);
            repeatCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // 동시에 같은 몹 5마리 생성
    public void SpawnMob(int _a)
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(mobPrefab[_a], spawnPoints[i].position, spawnPoints[i].rotation, parent.transform);
        }
    }

}
