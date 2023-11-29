using System.Collections;
using TMPro;
using UnityEngine;

public class IntroUIManager : MonoBehaviour
{
    public TextMeshProUGUI tapToStartText;
    public TextMeshProUGUI enterNicknameText;
    public TextMeshProUGUI limitCharactersText;
    public TextMeshProUGUI inputHereText;
    public TextMeshProUGUI okText;
    public TextMeshProUGUI yesText;
    public TextMeshProUGUI noText;
    public TextMeshProUGUI confirmNickname;

    public AudioClip introMusic;

    private void Awake()
    {
        SetResolution();
        GameManager.Create();
        LocalizationManager.Create();
        //SoundManager.Create();
    }
    private void Start()
    {
        GameManager.Instance.language = ES3.LoadString("language", GetLanguage());
        //GameManager.Instance.language = ES3.LoadString("language", "eng");
        UpdateLanguage(GameManager.Instance.language);
        SoundManager.Instance.PlayMusic(introMusic);
    }

    // 언어 갱신
    public void UpdateLanguage(string lang)
    {
        LocalizationManager.Instance.LoadLocalizedText(lang);
        GameManager.Instance.language = lang;

        tapToStartText.text = LocalizationManager.Instance.GetLocalizedValue("intro.tapToStart");
        enterNicknameText.text = LocalizationManager.Instance.GetLocalizedValue("nickname.enterNickname");
        limitCharactersText.text = LocalizationManager.Instance.GetLocalizedValue("nickname.limitCharacters");
        inputHereText.text = LocalizationManager.Instance.GetLocalizedValue("nickname.inputHere");
        okText.text = LocalizationManager.Instance.GetLocalizedValue("common.ok");
        yesText.text = LocalizationManager.Instance.GetLocalizedValue("common.yes");
        noText.text = LocalizationManager.Instance.GetLocalizedValue("common.no");
        confirmNickname.text = LocalizationManager.Instance.GetLocalizedValue("nickname.confirmNickname");
    }

    string GetLanguage()
    {
        SystemLanguage language = Application.systemLanguage;

        switch (language)
        {
            case SystemLanguage.English:
                return "eng";
            case SystemLanguage.Korean:
                return "kor";
            case SystemLanguage.Russian:
                return "rus";
            default:
                return "eng";
        }
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
