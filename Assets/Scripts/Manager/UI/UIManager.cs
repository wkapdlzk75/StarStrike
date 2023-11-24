using UnityEngine;

// 공통 UI 관리

public class UIManager : MonoBehaviour
{
    // 스테이지 프로퍼티
    public int CurrentStage
    {
        get { return GameManager.Instance.stage; }
        set { GameManager.Instance.stage = value; }
    }

    // 씬 변경 메소드
    public void SceneChange(string _SCENE_NAME)
    {
        SceneLoader.LoadScene(_SCENE_NAME);
    }
}
