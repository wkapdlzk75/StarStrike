using UnityEngine;
using UnityEngine.UI;

// 게임 씬 UI 관리

public class UIManagerGameScene : Manager
{
    public static UIManagerGameScene instance;

    public Text scoreText;      // 점수 UI
    public Text stageText;      // 스테이지 UI
    public Text popupText;      // 팝업 UI

    public Image[] lifeImage;   // 목숨 UI

    public GameObject popupUI;  // 팝업 UI

    // 점수 프로퍼티
    public int CurrentScore
    {
        get { return GameManager.instance.score; }
        //set { GameManager.instance.score = value; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        stageText.text = string.Format("스테이지 {0}", CurrentStage);
        UpdateScore();
        popupUI.SetActive(false);
    }

    // 점수 추가
    public void UpdateScore()
    {
        scoreText.text = string.Format("{0:N0}", CurrentScore);
    }

    // 목숨 UI 갱신
    public void UpdateLife(int _life)
    {
        lifeImage[_life].color = new Color(1, 1, 1, 0);   // 투명
        //lifeImage[_life].color = new Color(1, 1, 1, 1);   // 불투명
    }

    // 게임 종료
    public void EndGame()
    {
        popupUI.SetActive(true);
    }

}
