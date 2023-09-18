using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MobManager : SSSingleton<MobManager>
{
    public Mob[] mobPrefab;             // 몹 프리팹

    public GameObject parent;
    int stage;                          // 스테이지
    int repeatCount;                    // 적 스폰 반복 횟수
    public Transform[] spawnPoints;     // 적 스폰 위치
    public float spawnInterval;         // 적 스폰 시간 간격

    public Player player;               // 플레이어

    List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    private void Start()
    {
        spawnList = new List<Spawn>();
        stage = GameManager.instance.stage;
        repeatCount = 0;
        //ReadSpawnFile();
        Invoke("GameStart", 3);    // 게임 시작후 3초 뒤 몹 생성
    }

    // 파일 읽기
    void ReadSpawnFile()
    {
        // 변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // 파일 읽기
        TextAsset textFile = Resources.Load("Stage 1") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();

            if (line == null)
                break;

            // 리스폰 데이터 생성
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        // 텍스트 파일 닫기
        stringReader.Close();

        // 첫번째 스폰 딜레이 적용
        //spawnInterval = spawnList[0].delay;

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
                int rangeMob = Random.Range(0, stage);
                SpawnMob(rangeMob);
                repeatCount++;
                yield return new WaitForSeconds(spawnInterval);
            }
            yield return new WaitForSeconds(3);
            GameManager.instance.VictoryGame();
        }
        else if (stage == 4)
        {
            while (repeatCount < 15)
            {
                int rangeMob = Random.Range(0, 3);
                SpawnMob(rangeMob);
                SideSpawnMob(rangeMob); // 50%의 확률로 스폰
                repeatCount++;
                yield return new WaitForSeconds(spawnInterval);
            }
            yield return new WaitForSeconds(5);
            GameManager.instance.VictoryGame();
        }
        else if (stage == 5)
        {
            while (repeatCount < 0)
            {
                int rangeMob = Random.Range(0, 3);
                SpawnMob(rangeMob);
                SideSpawnMob(rangeMob); // 50%의 확률로 스폰
                repeatCount++;
                yield return new WaitForSeconds(spawnInterval);
            }
            Debug.Log("7초 뒤 보스 스폰");
            yield return new WaitForSeconds(7);
            spawnBoss();

        }

    }

    void spawnBoss()
    {
        //ObjectManager.Instance.PushRangedObject("MobBoss",);

        Mob mob = Instantiate(mobPrefab[3], spawnPoints[2].position, spawnPoints[2].rotation, parent.transform);
        mob.player = player;
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

    // 몹 사이드 랜덤 스폰
    public void SideSpawnMob(int _a)
    {
        int probability = Random.Range(0, 2);
        if (probability == 0)
        {
            int rangeMob = Random.Range(5, 9);
            Mob mob = Instantiate(mobPrefab[_a], spawnPoints[rangeMob].position, spawnPoints[rangeMob].rotation, parent.transform);
            mob.player = player;
            if (rangeMob == 5 || rangeMob == 6)         // 5, 6은 오른쪽 사이드 스폰
                mob.MoveSide(Vector2.left);             // 왼쪽으로 이동
            else if (rangeMob == 7 || rangeMob == 8)    // 7, 8은 왼쪽 사이드 스폰
                mob.MoveSide(Vector2.right);            // 오른쪽으로 이동

        }

    }

}
