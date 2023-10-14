using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false; // 일시정지 상태인지 확인하는 플래그
    public GameObject popupUI;
    public void TogglePause()
    {
        isPaused = !isPaused; // 상태를 반전
        if (isPaused)
        {
            Pause();
        }
        else
        {
            ResumeGame();
        }
    }

    public void Pause()
    {
        popupUI.SetActive(true);
        Time.timeScale = 0f; // 게임 일시정지
        isPaused = true;
    }

    public void ResumeGame()
    {
        popupUI.SetActive(false);
        Time.timeScale = 1f; // 게임 재개
        isPaused = false;
    }
}
