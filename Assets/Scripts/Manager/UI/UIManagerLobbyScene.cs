using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 로비 씬 UI 관리

public class UIManagerLobbyScene : UIManager
{
    public static UIManagerLobbyScene Instance;
    
    public TextMeshProUGUI stageIntTMP;
    public TextMeshProUGUI stageTMP;
    public TextMeshProUGUI closeTMP1;

    public TextMeshProUGUI rankingTMP1;
    public TextMeshProUGUI rankingTMP2;
    public TextMeshProUGUI rankingTMP3;

    public TextMeshProUGUI gameStartTMP;
    
    public TextMeshProUGUI optionTMP;
    public TextMeshProUGUI masterVolumeTMP;

    public TextMeshProUGUI storeTMP;
    public TextMeshProUGUI closeTMP2;
    
    public Text goldText1;
    public Text goldText2;
    public Text boomText;

    public GameObject store;

    const int MAX_STAGE = 5;    // 마지막 스테이지

    private void Awake()
    {
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
        SoundManager.Instance.PlayLobbyMusic();
        SetResolution();
        
    }

    IEnumerator LoadData()
    {
        yield return new WaitUntil(() => SaveLoadManager.loadEnd);
        UIUpdateStage();
        UpdateGoods();
        UpdateLanguage(GameManager.Instance.language);
    }

    // 언어 갱신
    public void UpdateLanguage(string _lang)
    {
        LocalizationManager.Instance.LoadLocalizedText(_lang);
        GameManager.Instance.language = _lang;

        stageTMP.text = LocalizationManager.Instance.GetLocalizedValue("common.stage");
        closeTMP1.text = LocalizationManager.Instance.GetLocalizedValue("common.close");

        rankingTMP1.text = LocalizationManager.Instance.GetLocalizedValue("lobby.ranking");
        gameStartTMP.text = LocalizationManager.Instance.GetLocalizedValue("lobby.gameStart");

        optionTMP.text = LocalizationManager.Instance.GetLocalizedValue("option.option");
        masterVolumeTMP.text = LocalizationManager.Instance.GetLocalizedValue("option.masterVolume");

        rankingTMP2.text = LocalizationManager.Instance.GetLocalizedValue("ranking.ranking");
        rankingTMP3.text = LocalizationManager.Instance.GetLocalizedValue("ranking.no-value");

        storeTMP.text = LocalizationManager.Instance.GetLocalizedValue("store.store");
        closeTMP2.text = LocalizationManager.Instance.GetLocalizedValue("common.close");

        store.GetComponent<Store>().Refresh();
    }

    // 재화 갱신
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
        stageIntTMP.text = CurrentStage.ToString();
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




