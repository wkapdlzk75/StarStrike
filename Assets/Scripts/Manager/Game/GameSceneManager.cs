using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public Transform startPos;

    void Start()
    {
        GameManager.Instance.player.transform.position = startPos.position;
        GameManager.Instance.GameStart();
        SoundManager.Instance.PlayGameMusic();
    }

}
