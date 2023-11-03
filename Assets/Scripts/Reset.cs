using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public void Onclick()
    {
        PlayerPrefs.DeleteAll();
        ES3.DeleteFile();

        // 에디터에서 실행 중일 때
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // 빌드된 게임에서 실행 중일 때
        Application.Quit();
        #endif
    }
}
