using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int stage;   // 스테이지
    public int score;   // 점수

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

    void Start()
    {
        stage = 1;
    }

    // 게임 승리
    public void VictoryGame()
    {
        score = 0;
        UIManagerGameScene.instance.VictoryGame();
    }

    // 점수 추가 및 UI 갱신
    public void AddScore(int _score)
    {
        score += _score;
        UIManagerGameScene.instance.UpdateScore();
    }

    // 게임 종료
    public void EndGame()
    {
        score = 0;
        UIManagerGameScene.instance.EndGame();
    }

}
