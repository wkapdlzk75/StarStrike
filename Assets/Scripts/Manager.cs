using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public void SceneChange(string _SCENE_NAME)
    {
        SceneManager.LoadScene(_SCENE_NAME);
    }
}
