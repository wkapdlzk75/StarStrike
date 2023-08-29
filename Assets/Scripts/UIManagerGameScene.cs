using UnityEngine;
using UnityEngine.UI;

// 게임 씬 UI 관리

public class UIManagerGameScene : Manager
{
    public static UIManagerGameScene instance;

    public Text scoreText;  // 점수 UI
    public Text stageText;  // 스테이지 UI
    public Text popupText;  // 팝업 UI

    public GameObject popupUI;

    // 점수 프로퍼티
    public int CurrentScore
    {
        get { return GameManager.instance.score; }
        set { GameManager.instance.score = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        stageText.text = string.Format("스테이지 {0}", CurrentStage);
        scoreText.text = CurrentScore.ToString();
        popupUI.SetActive(false);
    }

    // 점수 추가
    public void Addscore(int _score)
    {
        CurrentScore += _score;
        scoreText.text = string.Format("{0:N0}", CurrentScore);
    }

    // 게임 종료
    public void EndGame()
    {
        popupUI.SetActive(true);
    }

}
