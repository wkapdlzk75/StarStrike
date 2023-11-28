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

    bool loadEnd;

    private void Awake()
    {
        GameManager.Create();
        LocalizationManager.Create();
        SoundManager.Create();
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
}
