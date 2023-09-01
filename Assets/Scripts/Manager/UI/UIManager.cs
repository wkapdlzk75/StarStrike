using UnityEngine;
using UnityEngine.SceneManagement;

// 공통 UI 관리

public class UIManager : MonoBehaviour
{
    // 스테이지 프로퍼티
    public int CurrentStage
    {
        get { return GameManager.instance.stage; }
        set { GameManager.instance.stage = value; }
    }

    // 씬 변경 메소드
    public void SceneChange(string _SCENE_NAME)
    {
        SceneManager.LoadScene(_SCENE_NAME);
    }
}
