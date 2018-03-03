using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour {
    public AudioClip[] bgm;
    public AudioClip bossMusic;
    public AudioSource audioSource;
    public float volume;
    public bool isMuted;

    int currentSong;

    void Awake()
    {
        currentSong = 0;
        audioSource.clip = bgm[currentSong];
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void play()
    {
        audioSource.Play();
    }

    public void nextSong()
    {
        currentSong += 1;
        currentSong = currentSong % bgm.Length;
        audioSource.clip = bgm[currentSong];
        play();
    }

    public void previousSong()
    {
        currentSong -= 1;
        if (currentSong < 0)
        {
            currentSong = bgm.Length - 1;
        }
        audioSource.clip = bgm[currentSong];
        play();
    }

    public void mute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
    }
}
