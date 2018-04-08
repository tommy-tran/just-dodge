using System.Collections;
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
    public DialogueTrigger winDialogueTrigger;
    public GameObject gameoverBox;
    public GameObject gameoverText;
    public GameObject scoreText;
    public ScoreController scoreController;

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
        if (level == 10)
        {
            startBoss();
            yield return new WaitForSeconds(4f);
            StartCoroutine(waitForBossDeath());
        } else
        {
            spawnWave();
            StartCoroutine(waitForWave());
        }
        
    }

    IEnumerator waitForWave()
    {
        yield return new WaitUntil(() => spawnController.remaining <= 0);
        yield return new WaitForSeconds(4f);
        dialogueTrigger[level].TriggerDialog(false);
        StartCoroutine(waitForDialogue());
    }

    IEnumerator waitForBossDeath()
    {
        yield return new WaitUntil(() => spawnController.bossAlive == false);
        win();
    }

    void spawnWave()
    {
        spawnController.StartWave(level++);
    }

    void startBoss()
    {
        soundController.startBossTheme();
        spawnController.spawnBoss();
    }

    void win()
    {
        gameoverText.GetComponent<Text>().text = "You win!";
        scoreController.setScore(true);
        gameoverBox.SetActive(true);
        StopAllCoroutines();
        dialogueManager.StopAllCoroutines();
        spawnController.StopAllCoroutines();
        winDialogueTrigger.TriggerDialog(false);
    }

    public void GameOver()
    {
        gameoverText.GetComponent<Text>().text = "Game Over";
        scoreController.setScore(false);
        gameoverBox.SetActive(true);
        StopAllCoroutines();
        dialogueManager.StopAllCoroutines();
        spawnController.StopAllCoroutines();
        GODialogueTrigger.TriggerDialog(false);
    }

    public void Retry()
    {
        Start();
        scoreController.resetScore();
        gameoverBox.SetActive(false);
        GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Ally"))
        {
            Destroy(enemy);
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Boss"))
        {
            Destroy(enemy);
        }
        playerController.retry();
    }
}
