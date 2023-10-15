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

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayLobbyMusic()
    {
        audioSource.PlayOneShot(lobbyMusic);
    }
    public void PlayGameMusic()
    {
        audioSource.PlayOneShot(gameMusic);
    }
    public void PlayBossMusic()
    {
        audioSource.PlayOneShot(bossMusic);
    }
}
