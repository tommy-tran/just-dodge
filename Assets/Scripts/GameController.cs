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
    public DialogueTrigger healthDialogueTrigger;
    public GameObject gameoverBox;
    public GameObject gameoverText;
    public GameObject scoreText;
    public ScoreController scoreController;
    bool gotHealth;

    int level;

    void Start()
    {
        level = 0;
        dialogueTrigger[level].TriggerDialog(true);
        gotHealth = false;
        StartCoroutine(waitForDialogue());
    }

    public void Quit()
    {
        Application.Quit();
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

        yield return new WaitForSeconds(Random.Range(5f, 9f));
        if (playerController.health < 2 && level % 3 == 0 && !gotHealth)
        {
            if (Random.Range(0, 5) < 3)
            {
                healthDialogueTrigger.TriggerDialog(false);
                spawnController.spawnHealth();
                gotHealth = true;
            }
            else if (level == 9)
            {
                healthDialogueTrigger.TriggerDialog(false);
                spawnController.spawnHealth();
                gotHealth = true;
            }
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
        scoreEnemies();
        scoreController.win();
        gameoverBox.SetActive(true);
        StopAllCoroutines();
        dialogueManager.StopAllCoroutines();
        spawnController.StopAllCoroutines();
        winDialogueTrigger.TriggerDialog(false);
    }

    public void GameOver()
    {
        gameoverText.GetComponent<Text>().text = "Game Over";
        scoreController.setScore();
        gameoverBox.SetActive(true);
        StopAllCoroutines();
        dialogueManager.StopAllCoroutines();
        spawnController.StopAllCoroutines();
        GODialogueTrigger.TriggerDialog(false);
    }

    public void clearEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        foreach (GameObject minion in GameObject.FindGameObjectsWithTag("Minion"))
        {
            Destroy(minion);
        }

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            Destroy(item);
        }

        foreach (GameObject ally in GameObject.FindGameObjectsWithTag("Ally"))
        {
            Destroy(ally);
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Boss"))
        {
            Destroy(enemy);
        }
    }

    public void scoreEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            scoreController.dodgedEnemy();
            Destroy(enemy);
        }

        foreach (GameObject minion in GameObject.FindGameObjectsWithTag("Minion"))
        {
            scoreController.dodgedMinion();
            Destroy(minion);
        }

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            Destroy(item);
        }

        foreach (GameObject ally in GameObject.FindGameObjectsWithTag("Ally"))
        {
            Destroy(ally);
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Boss"))
        {
            Destroy(enemy);
        }
    }

    public void Retry()
    {
        Start();
        scoreController.resetScore();
        scoreController.setScore();
        gameoverBox.SetActive(false);
        clearEnemies();
        playerController.retry();
    }
}
