using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SSSingleton<SoundManager>
{
    public AudioClip lobbyMusic;
    public AudioClip gameMusic;
    public AudioClip bossMusic;
    public AudioSource audioSource;

    public float bgVolume;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        bgVolume = 0.25f;
        audioSource.volume = GameManager.Instance.masterVolume * bgVolume;
    }

    private void Update()
    {
        
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayLobbyMusic()
    {
        StopMusic();
        audioSource.PlayOneShot(lobbyMusic, GameManager.Instance.masterVolume * bgVolume);
        //audioSource.volume = 0;
    }

    public void PlayGameMusic()
    {
        StopMusic();
        audioSource.PlayOneShot(gameMusic, GameManager.Instance.masterVolume * bgVolume);
    }

    public void PlayBossMusic()
    {
        StopMusic();
        audioSource.PlayOneShot(bossMusic, GameManager.Instance.masterVolume * bgVolume);
    }
}
