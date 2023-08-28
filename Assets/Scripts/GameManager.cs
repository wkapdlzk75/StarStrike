using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text point;
    public Text stage;
    public Text popup;
    public static int pointInt;

    public GameObject popupUI;
    public GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        pointInt = 0;
        point.text = pointInt.ToString();
        popupUI.SetActive(false);

        try
        {
            stage.text = string.Format("스테이지 {0}", LobbyManager.instance.stageInt);
        }
        catch (NullReferenceException e)
        {
            stage.text = string.Format("스테이지 {0}", 1);
            //Debug.LogError("Player reference is null: " + e.Message);
        }
    }

    // 점수 추가
    public void AddPoint()
    {
        point.text = (++pointInt).ToString();
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
