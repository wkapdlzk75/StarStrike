using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : Manager
{
    public static LobbyManager instance;
    public int stageInt = 1; // 스테이지

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

    public Text stage;

    void Start()
    {
        stage.text = stageInt.ToString();
    }

    // 스테이지 다운
    public void StageDown()
    {
        if (stageInt <= 1)
            return;
        stage.text = (--stageInt).ToString();
    }

    // 스테이지 업
    public void StageUp()
    {
        if (stageInt >= 4)
            return;
        stage.text = (++stageInt).ToString();
    }

}
