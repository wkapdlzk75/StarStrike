using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 게임 씬 UI 관리

public class UIManagerGameScene : UIManager
{
    public static UIManagerGameScene instance;

    public Text scoreText1;      // 점수 UI
    public Text stageText;      // 스테이지 UI
    public Text popupText;      // 팝업 UI
    public Text goldText;       // 골드 텍스트 UI
    public Text scoreText2;
    public Text saveText;
    //public InputField inputName;
    public TMP_InputField inputName;


    public Image[] lifeImage;   // 목숨 UI
    public Image[] boomImage;   // 폭탄 UI

    public GameObject popupUI;  // 팝업 UI

    // 점수 프로퍼티
    public int CurrentScore
    {
        get { return GameManager.Instance.curScore; }
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
        UpdateGold();
        UpdateBoom(GameManager.Instance.GetResourceAmount(GameManager.EResource.boom));
        popupUI.SetActive(false);
    }

    // 점수 갱신
    public void UpdateScore()
    {
        scoreText1.text = string.Format("{0:N0}", CurrentScore);
    }

    // 골드 갱신
    public void UpdateGold()
    {
        goldText.text = string.Format("{0:N0}", GameManager.Instance.inGameGold);
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
    public void UpdateBoom(int _boom)
    {
        // 모든 이미지를 먼저 비활성화
        for (int i = 0; i < boomImage.Length; i++)
        {
            boomImage[i].gameObject.SetActive(false);
        }

        // _boom 갯수만큼만 활성화
        for (int i = 0; i < _boom; i++)
        {
            if (i < boomImage.Length) // 배열의 범위를 벗어나지 않게 체크
                boomImage[i].gameObject.SetActive(true);
        }
    }

    public void SaveAndGoLobby()
    {
        if (inputName.text == "")
        {
            Ranking.SaveScore("Unknown", CurrentScore);
        }
        else
        {
            Ranking.SaveScore(inputName.text, CurrentScore);
            //Ranking.SaveScore(inputName.text, int.Parse(scoreText1.text.Replace(",","")));
        }

        SceneChange("LobbyScene");
    }


    // 게임 종료
    public void EndGame(bool game)
    {
        if (game)
        {
            popupText.text = "Victory";
        }
        else
        {
            popupText.text = "Defeat";
        }

        scoreText2.text = "현재 점수 : " + scoreText1.text;

        popupUI.SetActive(true);
    }

}
