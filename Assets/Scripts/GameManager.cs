using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text meter;
    public Text point;
    public Text stage;
    public Text popup;
    public static int pointInt;

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
        point.text = string.Format("점수 : {0}", pointInt);

        try
        {
            stage.text = RobbyManager.instance.stageInt.ToString();
        }
        catch (NullReferenceException e)
        {
            stage.text = "1";
            Debug.LogError("Player reference is null: " + e.Message);
        }
        
        popup.text = "";
    }
    void Update()
    {
        meter.text = string.Format("미터 : {0:F2}", Time.time);
    }

    // 점수 추가
    public void AddPoint()
    {
        point.text = string.Format("점수 : {0}", ++pointInt);
    }

    public void RespawnPlayer()
    {
        player.transform.position = new Vector2(0, -4);
        player.SetActive(true);
    }

    // 게임 종료
    public void EndGame()
    {
        popup.text = "패배";
    }
}
