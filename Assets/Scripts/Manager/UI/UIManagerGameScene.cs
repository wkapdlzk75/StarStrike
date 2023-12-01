using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 게임 씬 UI 관리

public class UIManagerGameScene : UIManager
{
    public static UIManagerGameScene instance;

    public Text scoreText1;      // 점수 UI
    public Text stageText;      // 스테이지 UI
    public TextMeshProUGUI popupText;      // 팝업 UI
    public Text goldText;       // 골드 텍스트 UI
    public TextMeshProUGUI scoreText2;
    public TMP_InputField inputName;

    public TextMeshProUGUI inputNicknameTMP;
    public TextMeshProUGUI saveExitTMP1;
    public TextMeshProUGUI saveExitTMP2;
    public TextMeshProUGUI pauseTMP;

    public Image[] lifeImage;   // 목숨 UI
    public Image[] boomImage;   // 폭탄 UI

    public GameObject gameEndUI;
    public GameObject gamePauseUI;

    public GameObject gamePauseButton;
    public GameObject controller;

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
        stageText.text = string.Format("{0} {1}", LocalizationManager.Instance.GetLocalizedValue("common.stage"), CurrentStage);
        UpdateScore();
        UpdateGold();
        UpdateBoom(GameManager.Instance.GetResourceAmount(GameManager.EResource.boom));
        RefreshLanguage();
        gameEndUI.SetActive(false);
    }

    // 언어 갱신
    void RefreshLanguage()
    {
        inputNicknameTMP.text = LocalizationManager.Instance.GetLocalizedValue("inGame.inputNickname");
        saveExitTMP1.text = LocalizationManager.Instance.GetLocalizedValue("inGame.exit");
        saveExitTMP2.text = LocalizationManager.Instance.GetLocalizedValue("inGame.exit");
        pauseTMP.text = LocalizationManager.Instance.GetLocalizedValue("inGame.pause");
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

    public void ReturnLobby()
    {
        /*if (inputName.text == "")
        {
            Ranking.SaveScore("Unknown", int.Parse(scoreText1.text.Replace(",", "")));
        }
        else
        {
            Ranking.SaveScore(inputName.text, int.Parse(scoreText1.text.Replace(",", "")));
        }*/

        Ranking.SaveScore(GameManager.Instance.userNickname, int.Parse(scoreText1.text.Replace(",", "")));
        SceneChange("LobbyScene");
    }

    public void GiveUpGame()
    {
        Time.timeScale = 1f;
        gamePauseUI.gameObject.SetActive(false);
        GameManager.Instance.EndGame(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f; // 게임 일시정지
        gamePauseUI.gameObject.SetActive(true);
        gamePauseButton.gameObject.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gamePauseUI.gameObject.SetActive(false);
        gamePauseButton.gameObject.SetActive(true);
    }

    


    // 게임 종료
    public void EndGame(bool game)
    {
        if (game)
        {
            popupText.text = LocalizationManager.Instance.GetLocalizedValue("inGame.victory");
        }
        else
        {
            popupText.text = LocalizationManager.Instance.GetLocalizedValue("inGame.defeat");
        }
        scoreText2.text = LocalizationManager.Instance.GetLocalizedValue("inGame.currentScore") + " : " + scoreText1.text;

        gameEndUI.SetActive(true);
    }

}
