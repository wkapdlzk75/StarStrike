using UnityEngine;

public class GameManager : Manager
{
    public static GameManager instance;

    public const int MAX_STAGE = 4;     // 마지막 스테이지
    public int stage;                   // 스테이지
    public int score;                // 점수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    // 점수 추가 및 UI 갱신
    public void AddScore(int _score)
    {
        score += _score;
        UIManagerGameScene.instance.UpdateScore();
    }

}
