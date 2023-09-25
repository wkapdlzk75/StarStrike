using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MobSpawnManager : MonoBehaviour
{
    public struct st_MobData
    {
        public int key;
        public string name;
        public int max_Hp;
        public int damage;
        public float speed;
        public float firingInterval;
        public int score;
    }

    public GameObject parent;
    int stage;                          // 스테이지
    int repeatCount;                    // 적 스폰 반복 횟수
    public Transform[] spawnPoints;     // 적 스폰 위치
    public float spawnInterval;         // 적 스폰 시간 간격

    public Player player;               // 플레이어

    List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    // string[] mobPrefabString = { "MobS", "MobM", "MobL" };

    

    protected void Awake()
    {
        MobDataManager.Instance.CreateMobData();
    }

    private void OnEnable()
    {
        spawnList = new List<Spawn>();
        stage = GameManager.Instance.stage;
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
                SpawnMob("MobS");
                repeatCount++;
                yield return new WaitForSeconds(spawnInterval);
            }
            yield return new WaitForSeconds(3);
            GameManager.Instance.EndGame(true);
        }
        else if (stage == 4)
        {
            while (repeatCount < 15)
            {
                SpawnMob("MobS");
                SideSpawnMob("MobS"); // 50%의 확률로 스폰
                repeatCount++;
                yield return new WaitForSeconds(spawnInterval);
            }
            yield return new WaitForSeconds(5);
            GameManager.Instance.EndGame(true);
        }
        else if (stage == 5)
        {
            while (repeatCount < 0)
            {
                int rangeMob = Random.Range(0, 3);
                SpawnMob("MobS");
                SideSpawnMob("MobS"); // 50%의 확률로 스폰
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
        ObjectManager.Instance.GetRangedObject("MobBoss", (poolingMob) =>
        {
            poolingMob.transform.position = spawnPoints[2].position;
            poolingMob.transform.rotation = spawnPoints[2].rotation;
            Mob mob = poolingMob.GetComponent<Mob>();

            mob.player = player;
            mob.mobInit();
        });
    }

    // 동시에 같은 몹 5마리 생성
    public void SpawnMob(string mobName)
    {
        for (int i = 0; i < 5; i++)
        {
            int temp = i;
            ObjectManager.Instance.GetRangedObject(MobDataManager.Instance.mobDataDic[mobName].name, (poolingMob) =>
            {
                poolingMob.transform.position = spawnPoints[temp].position;
                poolingMob.transform.rotation = spawnPoints[temp].rotation;
                Mob mob = poolingMob.GetComponent<Mob>();
                mob.player = player;
                mob.mobInit();
            });
        }
    }

    // 몹 사이드 랜덤 스폰
    public void SideSpawnMob(string mobName)
    {
        int probability = Random.Range(0, 2);

        if (probability == 0)
        {
            int rangeMob = Random.Range(5, 9);

            ObjectManager.Instance.GetRangedObject(MobDataManager.Instance.mobDataDic[mobName].name, (pooling) =>
            {
                pooling.transform.position = spawnPoints[rangeMob].position;
                pooling.transform.rotation = spawnPoints[rangeMob].rotation;

                Mob mob = pooling.GetComponent<Mob>();
                mob.player = player;
                mob.mobInit();

                if (rangeMob == 5 || rangeMob == 6)         // 5, 6은 오른쪽 사이드 스폰
                    mob.MoveSide(Vector2.left);             // 왼쪽으로 이동
                else if (rangeMob == 7 || rangeMob == 8)    // 7, 8은 왼쪽 사이드 스폰
                    mob.MoveSide(Vector2.right);            // 오른쪽으로 이동
            });

        }

    }

}
