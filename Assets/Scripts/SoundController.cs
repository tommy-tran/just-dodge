using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour {
    public AudioClip[] bgm;
    public AudioClip bossMusic;
    public AudioClip damageSound;
    public AudioSource musicSource;
    public AudioSource soundSource;
    public float volume;
    public bool isMuted;

    int currentSong;

    void Awake()
    {
        currentSong = 0;
        musicSource.clip = bgm[currentSong];
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void playDamageSound()
    {
        soundSource.clip = damageSound;
        soundSource.Play();
    }

    public void play()
    {
        musicSource.Play();
    }

    public void nextSong()
    {
        currentSong += 1;
        currentSong = currentSong % bgm.Length;
        musicSource.clip = bgm[currentSong];
        play();
    }

    public void previousSong()
    {
        currentSong -= 1;
        if (currentSong < 0)
        {
            currentSong = bgm.Length - 1;
        }
        musicSource.clip = bgm[currentSong];
        play();
    }

    public void mute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            musicSource.mute = true;
        }
        else
        {
            musicSource.mute = false;
        }
    }
}
