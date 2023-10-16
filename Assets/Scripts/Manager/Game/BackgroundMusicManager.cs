using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : SSSingleton<BackgroundMusicManager>
{
    public AudioClip lobbyMusic;
    public AudioClip gameMusic;
    public AudioClip bossMusic;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.volume = GameManager.Instance.volume * 0.2f;
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayLobbyMusic()
    {
        audioSource.PlayOneShot(lobbyMusic, GameManager.Instance.volume*0.2f);
        //audioSource.volume = 0;
    }

    public void PlayGameMusic()
    {
        audioSource.PlayOneShot(gameMusic, GameManager.Instance.volume * 0.2f);
    }

    public void PlayBossMusic()
    {
        audioSource.PlayOneShot(bossMusic, GameManager.Instance.volume * 0.2f);
    }
}
