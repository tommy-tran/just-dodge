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
    public float soundVolume;
    int caseNum;

    int currentSong;

    void Awake()
    {
        currentSong = 0;
        soundSource.volume = soundVolume;
        musicSource.clip = bgm[currentSong];
        musicSource.volume = volume;
        musicSource.Play();
        caseNum = 0;
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
        caseNum = (caseNum + 1) % 3;
        switch(caseNum)
        {
            case 0:
                musicSource.mute = false;
                soundSource.mute = false;
                break;
            case 1:
                musicSource.mute = true;
                soundSource.mute = false;
                break;
            case 2:
                musicSource.mute = true;
                soundSource.mute = true;
                break;
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
