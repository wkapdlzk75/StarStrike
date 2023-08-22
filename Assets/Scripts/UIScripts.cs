using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{
    int stageInt = 1;
    public Text stage;

    void Start()
    {
        stage.text = stageInt.ToString();
    }

    public void StageDown()
    {
        if (stageInt <= 1)
            return;
        stage.text = (--stageInt).ToString();
    }

    public void StageUp()
    {
        if (stageInt >= 10)
            return;
        stage.text = (++stageInt).ToString();
    }



    public void OnClick()
    {
        SceneManager.LoadScene("Scenes/Main");
    }
}
