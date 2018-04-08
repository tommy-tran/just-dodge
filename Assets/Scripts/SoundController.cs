using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour {
    public AudioClip[] bgm;
    public AudioClip bossMusic;
    public AudioClip damageSound;
    public AudioClip coinSound;
    public AudioClip diamondSound;
    public AudioClip soldierSound;
    public AudioClip soldierDieSound;
    public AudioClip firstAidSound;
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

    public void playSoldierSound()
    {
        soundSource.clip = soldierSound;
        soundSource.Play();
    }

    public void playSoldierDieSound()
    {
        soundSource.clip = soldierDieSound;
        soundSource.Play();
    }

    public void playDamageSound()
    {
        soundSource.clip = damageSound;
        soundSource.Play();
    }

    public void playCoinSound()
    {
        soundSource.clip = coinSound;
        soundSource.Play();
    }

    public void playDiamondSound()
    {
        soundSource.clip = diamondSound;
        soundSource.Play();
    }

    public void playHealSound()
    {
        soundSource.clip = firstAidSound;
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

    public void startBossTheme()
    {
        Array.Resize(ref bgm, bgm.Length + 1);
        bgm[bgm.Length - 1] = bossMusic;
        currentSong = bgm.Length - 1;
        musicSource.clip = bgm[currentSong];
        play();
    }
}
