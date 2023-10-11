using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 로비 씬 UI 관리

public class UIManagerLobbyScene : UIManager
{
    public static UIManagerLobbyScene Instance;
    public Text stageText;
    public Text goldText1;
    public Text goldText2;
    public Text boomText;
    public Text highScoreText;

    const int MAX_STAGE = 5;    // 마지막 스테이지

    private void Awake()
    {
        GameManager.Create();
        ObjectManager.Create();
        BackGroundManager.Create();
        CSVManager.Create();

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(LoadData());
    }

    IEnumerator LoadData()
    {
        yield return new WaitUntil(()=> SaveLoadManager.loadEnd);
        UIUpdateStage();
        UpdateGoods();
        UpdateHighScore();
    }

    // 골드 갱신
    public void UpdateGoods()
    {
        goldText1.text = string.Format("{0:N0}", GameManager.Instance.GetResourceAmount(GameManager.EResource.gold));
        goldText2.text = string.Format("{0:N0}", GameManager.Instance.GetResourceAmount(GameManager.EResource.gold));
        boomText.text = string.Format("{0:N0}", GameManager.Instance.GetResourceAmount(GameManager.EResource.boom));
    }

    public void UpdateHighScore()
    {
        highScoreText.text = string.Format("{0:N0}", GameManager.Instance.highScore);
    }

    // 스테이지 변경
    public void ChangeStage(int _value)
    {
        if ((CurrentStage <= 1 && _value == -1) || (CurrentStage >= MAX_STAGE && _value == 1))
            return;
        CurrentStage += _value;
        UIUpdateStage();
    }

    // 스테이지 텍스트 UI 갱신
    public void UIUpdateStage()
    {
        stageText.text = CurrentStage.ToString();
    }

}
