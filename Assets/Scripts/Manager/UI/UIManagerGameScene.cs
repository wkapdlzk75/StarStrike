using UnityEngine;
using UnityEngine.UI;

// 게임 씬 UI 관리

public class UIManagerGameScene : UIManager
{
    public static UIManagerGameScene instance;

    public Text scoreText;      // 점수 UI
    public Text stageText;      // 스테이지 UI
    public Text popupText;      // 팝업 UI

    public Image[] lifeImage;   // 목숨 UI
    public Image[] boomImage;   // 폭탄 UI

    public GameObject popupUI;  // 팝업 UI

    //public Player player;       // 플레이어 정보

    // 점수 프로퍼티
    public int CurrentScore
    {
        get { return GameManager.Instance.score; }
        //set { GameManager.Instance.score = value; }
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
    public void UpdateLife(int _life, bool _value)
    {
        if (_value) // true 추가
            lifeImage[_life - 1].gameObject.SetActive(true);
        else        // false 제거
            lifeImage[_life].gameObject.SetActive(false);
    }

    // 폭탄 UI 갱신
    public void UpdateBoom(int _boom, bool _value)
    {
        if (_value) // true 추가
            boomImage[_boom - 1].gameObject.SetActive(true);
        else        // false 제거
            boomImage[_boom].gameObject.SetActive(false);
    }

    public void VictoryGame()
    {
        popupText.text = "Victory";
        EndGame();
    }


    // 게임 종료
    public void EndGame()
    {
        popupUI.SetActive(true);
    }

}
