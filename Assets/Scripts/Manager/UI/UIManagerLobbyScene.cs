using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 로비 씬 UI 관리

public class UIManagerLobbyScene : UIManager
{
    public static UIManagerLobbyScene Instance;
    public TextMeshProUGUI stageText;
    public Text goldText1;
    public Text goldText2;
    public Text boomText;

    const int MAX_STAGE = 5;    // 마지막 스테이지

    private void Awake()
    {
        GameManager.Create();
        ObjectManager.Create();
        BackGroundManager.Create();
        CSVManager.Create();
        SoundManager.Create();

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(LoadData());
        SoundManager.Instance.PlayLobbyMusic();
        SetResolution();
    }

    IEnumerator LoadData()
    {
        yield return new WaitUntil(() => SaveLoadManager.loadEnd);
        UIUpdateStage();
        UpdateGoods();
    }

    // 골드 갱신
    public void UpdateGoods()
    {
        goldText1.text = string.Format("{0:N0}", GameManager.Instance.GetResourceAmount(GameManager.EResource.gold));
        goldText2.text = string.Format("{0:N0}", GameManager.Instance.GetResourceAmount(GameManager.EResource.gold));
        boomText.text = string.Format("{0:N0}", GameManager.Instance.GetResourceAmount(GameManager.EResource.boom));
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

    /// <summary>
    /// 해상도 고정 함수
    /// </summary>
    public void SetResolution()
    {
        int setWidth = 900 * 9 / 16; // 화면 너비
        int setHeight = 900; // 화면 높이

        //해상도를 설정값에 따라 변경
        //3번째 파라미터는 풀스크린 모드를 설정 > true : 풀스크린, false : 창모드
        Screen.SetResolution(setWidth, setHeight, false);
    }
}




