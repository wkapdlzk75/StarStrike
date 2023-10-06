using UnityEngine.UI;

// 로비 씬 UI 관리

public class UIManagerLobbyScene : UIManager
{
    public static UIManagerLobbyScene instance;
    public Text stageText;
    public Text goldText;

    const int MAX_STAGE = 5;    // 마지막 스테이지

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        UIUpdateStage();
        UpdateGold();
    }

    // 골드 갱신
    public void UpdateGold()
    {
        goldText.text = string.Format("{0:N0}", GameManager.Instance.GetResourceAmount(GameManager.EResource.gold));
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
