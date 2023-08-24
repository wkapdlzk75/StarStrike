using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{
    public Text stage;

    void Start()
    {
        stage.text = RobbyManager.instance.stageInt.ToString();
    }

    public void StageDown()
    {
        if (RobbyManager.instance.stageInt <= 1)
            return;
        stage.text = (--RobbyManager.instance.stageInt).ToString();
    }

    public void StageUp()
    {
        if (RobbyManager.instance.stageInt >= 3)
            return;
        stage.text = (++RobbyManager.instance.stageInt).ToString();
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
}
