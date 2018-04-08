using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootmanBehaviour : MonoBehaviour {
    public SpawnBehaviour spawnBehaviour;
    SoundController soundController;
    ScoreController scoreController;
    public bool saved;
    new Transform transform;
    Animator anim;
    

    void Start()
    {
        transform = gameObject.GetComponent<Transform>();
        soundController = GameObject.FindObjectOfType<SoundController>();
        scoreController = GameObject.FindObjectOfType<ScoreController>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isMoving", true);
        saved = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !saved)
        {
            saved = true;
            transform.LookAt(GameObject.FindGameObjectWithTag("Boss").transform);
            soundController.playSoldierSound();
            scoreController.gotAlly();
        }

        if (other.CompareTag("Boss") && saved) 
        {
            spawnBehaviour.movementSpeed = 0;
            soundController.playSoldierDieSound();
            anim.SetBool("isDead", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Boss") && saved)
        {
            spawnBehaviour.movementSpeed = 0;
            anim.SetBool("isDead", true);
        }
    }
}
