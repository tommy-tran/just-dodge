using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public SpawnController spawner;
    public PlayerController playerController;
    public DialogueManager dialogueManager;
    public DialogueTrigger[] dialogueTrigger;
    public DialogueTrigger GODialogueTrigger;
    public GameObject gameoverBox;
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
        yield return new WaitForSeconds(5f);
        spawnWave();
        StartCoroutine(waitForWave());
    }

    IEnumerator waitForWave()
    {
        yield return new WaitUntil(() => spawner.remaining <= 0);
        yield return new WaitForSeconds(10f);
        dialogueTrigger[level].TriggerDialog(false);
        StartCoroutine(waitForDialogue());
    }

    void spawnWave()
    {
        spawner.StartWave(level++);
    }

    public void GameOver()
    {
        gameoverBox.SetActive(true);
        StopAllCoroutines();
        dialogueManager.StopAllCoroutines();
        spawner.StopAllCoroutines();
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
        playerController.PlayerAlive();
    }


}
