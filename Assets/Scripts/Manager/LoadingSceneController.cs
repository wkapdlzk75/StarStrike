using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    public Slider loadingBar; // 로딩 바 (UI 슬라이더)
    public TextMeshProUGUI loadingText; // 로딩 텍스트
    string text;

    void Start()
    {
        // 게임 씬 비동기 로드 시작
        StartCoroutine(LoadAsyncScene(SceneLoader.nextScene));
        text = LocalizationManager.Instance.GetLocalizedValue("loading.loading");
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // 로딩 진행도에 따라 UI 업데이트
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingBar.value = progress;
            loadingText.text = text + "     " + (progress * 100).ToString("F0") + "%";

            // 로딩이 거의 완료되었을 때 씬 활성화
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
