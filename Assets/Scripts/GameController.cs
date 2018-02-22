﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public SpawnController spawner;
    public DialogueManager dialogueManager;
    public DialogueTrigger[] dialogueTrigger;
    int level;

    void Start()
    {
        level = 0;
        dialogueManager.StartDialogue(dialogueTrigger[level].dialogue);
        StartCoroutine(waitForDialogue());
    }

    IEnumerator waitForDialogue()
    {
        yield return new WaitUntil(() => !dialogueManager.getState());
        yield return new WaitForSeconds(2f);
        spawnWave();
        StartCoroutine(waitForWave());
    }

    IEnumerator waitForWave()
    {
        yield return new WaitUntil(() => spawner.remaining <= 0);
        yield return new WaitForSeconds(4f);
        dialogueManager.StartDialogue(dialogueTrigger[level].dialogue);
        StartCoroutine(waitForDialogue());
    }

    void spawnWave()
    {
        spawner.StartWave(level++);
    }

    void GameOver()
    {

    }

}
