using System.Collections.Generic;
using UnityEngine;

public class GameManager : SSSingleton<GameManager>
{
    public int stage;   // 스테이지
    public int score;   // 점수
    
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

        ObjectManager.Create();
        BackGroundManager.Create();
        CSVManager.Create();
        //MobManager.Create();
    }

    void Start()
    {
        player.gameObject.SetActive(false);
        stage = 1;
    }

    // 게임시작
    public void GameStart()
    {
        player.gameObject.SetActive(true);
    }

    // 점수 추가 및 UI 갱신
    public void AddScore(int _score)
    {
        score += _score;
        UIManagerGameScene.instance.UpdateScore();
    }

    // 게임 종료
    public void EndGame(bool game)
    {
        player.fireAble = false;
        ObjectManager.Instance.AllPush();
        score = 0;
        UIManagerGameScene.instance.EndGame(game);
    }

}
