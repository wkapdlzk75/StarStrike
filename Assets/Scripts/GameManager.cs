using UnityEngine;

public class GameManager : Manager
{
    public static GameManager instance;

    public const int MAX_STAGE = 4;     // 마지막 스테이지
    public int stage;                   // 스테이지
    public int score;                // 점수

    public GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        stage = 1;
        score = 0;
    }

    // 플레이어 리스폰
    public void RespawnPlayer()
    {
        player.transform.position = new Vector2(0, -4);
        player.SetActive(true);
    }

    // 게임 종료
    public void EndGame()
    {
        //popupUI.SetActive(true);
    }

}
