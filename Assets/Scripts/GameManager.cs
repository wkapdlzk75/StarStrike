using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Manager
{
    public Text scoreText;
    public Text stageText;
    public Text popupText;
    int scoreInt;

    public GameObject popupUI;
    public GameObject player;
    void Start()
    {
        InitGame();
    }

    public void InitGame()
    {
        scoreInt = 0;
        scoreText.text = scoreInt.ToString();
        popupUI.SetActive(false);

        try
        {
            stageText.text = string.Format("스테이지 {0}", LobbyManager.instance.stageInt);
        }
        catch (NullReferenceException e)
        {
            stageText.text = string.Format("스테이지 {0}", 1);
            //Debug.LogError("Player reference is null: " + e.Message);
        }
    }

    // 점수 추가
    public void Addscore(int _score)
    {
        scoreInt += _score;
        scoreText.text = string.Format("{0:N0}", scoreInt);
    }

    public void RespawnPlayer()
    {
        player.transform.position = new Vector2(0, -4);
        player.SetActive(true);
    }

    // 게임 종료
    public void EndGame()
    {
        popupUI.SetActive(true);
    }

}
