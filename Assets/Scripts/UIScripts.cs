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
        stage.text = GameManager.instance.stageInt.ToString();
    }

    public void StageDown()
    {
        if (GameManager.instance.stageInt <= 1)
            return;
        stage.text = (--GameManager.instance.stageInt).ToString();
    }

    public void StageUp()
    {
        if (GameManager.instance.stageInt >= 10)
            return;
        stage.text = (++GameManager.instance.stageInt).ToString();
    }



    public void OnClick()
    {
        SceneManager.LoadScene("Scenes/Main");
    }
}
