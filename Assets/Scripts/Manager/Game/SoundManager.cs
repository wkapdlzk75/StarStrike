using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SSSingleton<SoundManager>
{
    public AudioClip lobbyMusic;
    public AudioClip gameMusic;
    public AudioClip bossMusic;
    private AudioSource audioSource;

    public float bgVolume = 1f;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        bgVolume = 0.25f;
        audioSource.volume = GameManager.Instance.wholeVolume * bgVolume;
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayLobbyMusic()
    {
        StopMusic();
        audioSource.PlayOneShot(lobbyMusic, GameManager.Instance.wholeVolume * bgVolume);
        //audioSource.volume = 0;
    }

    public void PlayGameMusic()
    {
        StopMusic();
        audioSource.PlayOneShot(gameMusic, GameManager.Instance.wholeVolume * bgVolume);
    }

    public void PlayBossMusic()
    {
        StopMusic();
        audioSource.PlayOneShot(bossMusic, GameManager.Instance.wholeVolume * bgVolume);
    }
}
