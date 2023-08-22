using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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


    /*public Text meter;
    public Text point;
    public Text stage;
    public Text popup;*/
    public static int pointInt;

    private void Start()
    {
        pointInt = 0;
        //point.text = string.Format("점수 : {0}", pointInt);
        //stage.text = stageInt.ToString();
        //popup.text = "";
    }

    public void EndGame()
    {
        //popup.text = "패배";
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Scenes/Robby");
        }
    }

    public void AddPoint()
    {
        //point.text = string.Format("점수 : {0}", ++pointInt);
    }

    /*private void Update()
    {
        meter.text = string.Format("미터 : {0:F2}", Time.time);
    }*/

}
