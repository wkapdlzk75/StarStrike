using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource shootingAudioSource;

    public void SetVolume(float volume)
    {
        shootingAudioSource.volume = volume;
    }
}
