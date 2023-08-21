using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Scenes/Main");
    }
}
