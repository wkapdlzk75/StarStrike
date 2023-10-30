using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnManager : MonoBehaviour
{
    int stage;                          // 스테이지
    public Transform[] spawnPoints;     // 적 스폰 위치

    List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    List<List<string>> spawnMob = new List<List<string>>();

    protected void Awake()
    {
        MobDataManager.Instance.CreateMobData();
        /*foreach (var key in MobDataManager.Instance.mobDataDic.Keys)
        {
            Debug.Log("Key: " + key + ", Value: " + MobDataManager.Instance.mobDataDic[key].name);
        }*/
    }

    private void OnEnable()
    {
        spawnList = new List<Spawn>();
        stage = GameManager.Instance.stage;
        //ReadSpawnFile();
        Invoke("GameStart", 3);    // 게임 시작후 3초 뒤 몹 생성
    }

    void ReadStage()
    {
        var data = CSVManager.Read("Stage/Stage " + stage.ToString());

        for (int i = 0; i < data.Count; i++)
        {
            List<string> list = new List<string>();
            for (int j = 0; j < data[i].Count; j++)
            {
                var temp = data[i][j.ToString()];
                list.Add(temp.ToString());

            }
            spawnMob.Add(list);
        }

    }

    // 게임 시작
    public void GameStart()
    {
        ReadStage();
        StartCoroutine(RepeatSpawnMob());
    }

    IEnumerator RepeatSpawnMob()
    {
        for (int i = 0; i < spawnMob.Count; i++)
        {
            for (int j = 0; j < spawnMob[i].Count; j++)
            {
                if (spawnMob[i][j] == "") continue;
                if (GameManager.Instance.isPlaying)
                    SpawnMob("Mob" + spawnMob[i][j], j);
            }
            yield return new WaitForSeconds(5);
        }

        // 스테이지 5인 경우에만 보스를 소환
        if (stage == 5)
        {
            SoundManager.Instance.PlayBossMusic();
            yield return new WaitForSeconds(4);  // 4초 대기
            SpawnBoss();  // 보스 소환
        }

        yield return new WaitUntil(() => GameManager.Instance.spawnCount == 0);

        GameManager.Instance.EndGame(true);
    }

    void SpawnBoss()
    {

        GameManager.Instance.spawnCount++;
        ObjectManager.Instance.GetRangedObject("MobB", (poolingMob) =>
        {
            poolingMob.transform.position = spawnPoints[2].position;
            poolingMob.transform.rotation = spawnPoints[2].rotation;
            Mob mob = poolingMob.GetComponent<Mob>();
            mob.player = GameManager.Instance.player;
            mob.InitializeMob();

            mob.deathAction = null;

            mob.deathAction += () =>
            {
                //spawnCount--;
            };

        });
    }


    void SpawnMob(string mobName, int spawnPos)
    {
        GameManager.Instance.spawnCount++;

        ObjectManager.Instance.GetRangedObject(MobDataManager.Instance.mobDataDic[mobName].name, (poolingMob) =>
        {
            poolingMob.transform.position = spawnPoints[spawnPos].position;
            poolingMob.transform.rotation = spawnPoints[spawnPos].rotation;

            Mob mob = poolingMob.GetComponent<Mob>();
            mob.player = GameManager.Instance.player;
            mob.InitializeMob();

            mob.deathAction = null;

            mob.deathAction += () =>
            {
                //spawnCount--;
            };

            if (spawnPos == 5 || spawnPos == 6)         // 오른쪽 사이드 스폰
                mob.MoveSide(Vector2.left);             // 왼쪽으로 이동
            else if (spawnPos == 7 || spawnPos == 8)    // 왼쪽 사이드 스폰
                mob.MoveSide(Vector2.right);            // 오른쪽으로 이동
        });
    }
}
