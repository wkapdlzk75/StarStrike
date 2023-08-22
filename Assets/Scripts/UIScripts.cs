using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{
    public Text stage;
    public GameManger gameManager;

    void Start()
    {
        stage.text = gameManager.stageInt.ToString();
    }

    public void StageDown()
    {
        if (gameManager.stageInt <= 1)
            return;
        stage.text = (--gameManager.stageInt).ToString();
    }

    public void StageUp()
    {
        if (gameManager.stageInt >= 10)
            return;
        stage.text = (++gameManager.stageInt).ToString();
    }



    public void OnClick()
    {
        SceneManager.LoadScene("Scenes/Main");
    }
}
