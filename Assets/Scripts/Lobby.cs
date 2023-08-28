using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Lobby : MonoBehaviour
{
    public static Lobby instance;
    public Text stage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        stage.text = LobbyManager.instance.stageInt.ToString();
    }

    // 스테이지 다운
    public void StageDown()
    {
        if (LobbyManager.instance.stageInt <= 1)
            return;
        stage.text = (--LobbyManager.instance.stageInt).ToString();
    }

    // 스테이지 업
    public void StageUp()
    {
        if (LobbyManager.instance.stageInt >= 4)
            return;
        stage.text = (++LobbyManager.instance.stageInt).ToString();
    }

    /*
    // 스테이지 다운
    public void StageDown()
    {
        LobbyManager.instance.StageDown();
        //if (stageInt <= 1)
        //    return;
        //stage.text = (--stageInt).ToString();
    }

    // 스테이지 업
    public void StageUp()
    {
        LobbyManager.instance.StageUp();
        //if (stageInt >= 4)
        //    return;
        //stage.text = (++stageInt).ToString();
    }*/
}
