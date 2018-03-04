﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public SpawnController spawnController;
    public SoundController soundController;
    public PlayerController playerController;
    public DialogueManager dialogueManager;
    public DialogueTrigger[] dialogueTrigger;
    public DialogueTrigger GODialogueTrigger;
    public GameObject gameoverBox;
    public GameObject scoreText;

    int level;

    void Start()
    {
        level = 0;
        dialogueTrigger[level].TriggerDialog(true);
        StartCoroutine(waitForDialogue());
    }

    IEnumerator waitForDialogue()
    {
        yield return new WaitUntil(() => !dialogueManager.getState());
        yield return new WaitForSeconds(4f);
        spawnWave();
        StartCoroutine(waitForWave());
    }

    IEnumerator waitForWave()
    {
        yield return new WaitUntil(() => spawnController.remaining <= 0);
        yield return new WaitForSeconds(5f);
        dialogueTrigger[level].TriggerDialog(false);
        StartCoroutine(waitForDialogue());
    }

    void spawnWave()
    {
        spawnController.StartWave(level++);
    }

    public void GameOver()
    {
        scoreText.GetComponent<Text>().text = (level).ToString();
        gameoverBox.SetActive(true);
        StopAllCoroutines();
        dialogueManager.StopAllCoroutines();
        spawnController.StopAllCoroutines();
        GODialogueTrigger.TriggerDialog(false);
    }

    public void Retry()
    {
        Start();
        gameoverBox.SetActive(false);
        GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
        playerController.retry();
    }


}
