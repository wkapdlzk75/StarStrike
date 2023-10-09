using System.Collections.Generic;
using UnityEngine;

public class GameManager : SSSingleton<GameManager>
{
    public int stage;       // 스테이지
    public int curScore;    // 점수
    public int highScore;   // 최대 점수
    public int inGameGold;

    public Dictionary<EResource, int> inventory = new Dictionary<EResource, int>();

    public enum EResource
    {
        gold,
        boom
    };

    public Player player;

    /*public Player Player
    {
        get
        {
            if (player == null)
            {
                var obj = GameObject.Find("Player");
                //GameObject

                if (obj != null)
                {
                    player = obj.GetComponent<Player>();
                }

                //player = GameObject.Find("Player").GetComponent<Player>();
            }
            return player;
        }

    }

    private Player player;*/


    protected override void Awake()
    {
        base.Awake();

        //GameObject.Find("awef");

        //int.TryParse(SaveLoadManager.Load(), out highScore);
        //MobManager.Create();
        //Debug.Log(highScore);
        SaveLoadManager.Load();

    }

    void Start()
    {
        //player.gameObject.SetActive(false);
        stage = 1;
    }

    public void AddResource(EResource eResource, int amount)
    {
        if (!inventory.ContainsKey(eResource))
        {
            inventory.Add(eResource, 0);
        }
        inventory[eResource] += amount;
    }

    public void RemoveResource(EResource eResource, int amount)
    {
        if (!inventory.ContainsKey(eResource))
        {
            inventory.Add(eResource, 0);
        }

        if (inventory[eResource] >= amount)
        {
            inventory[eResource] -= amount;
        }

    }

    public int GetResourceAmount(EResource eResource)
    {
        if (!inventory.ContainsKey(eResource))
        {
            inventory.Add(eResource, 0);
        }

        return inventory[eResource];
    }

    // 게임시작
    public void GameStart()
    {
        inGameGold = 0;
        //player.gameObject.SetActive(true);
    }

    // 점수 추가 및 UI 갱신
    public void AddScore(int _score)
    {
        curScore += _score;
        UIManagerGameScene.instance.UpdateScore();
    }

    // 게임 종료
    public void EndGame(bool game)
    {
        player.fireAble = false;
        ObjectManager.Instance.AllPush();
        AddResource(EResource.gold, inGameGold);


        if (highScore < curScore)
            highScore = curScore;

        //SaveLoadManager.Save(highScore);
        curScore = 0;
        SaveLoadManager.Save();
        UIManagerGameScene.instance.EndGame(game);
    }

}
