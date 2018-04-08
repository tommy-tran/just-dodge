using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {
    public SpawnBehaviour spawnBehaviour;
    public bool arrived;
    new Transform transform;
    Animator anim;
    SpawnController spawnController;
    int health;
    

    void Start()
    {
        transform = gameObject.GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isMoving", true);
        arrived = false;
        StartCoroutine(waitForEnter());
        health = 5;
        spawnController = GameObject.FindObjectOfType<SpawnController>();
    }

    void OnTriggerEnter(Collider other)
    {
        FootmanBehaviour soldier = other.GetComponentInParent<FootmanBehaviour>();
        if (other.CompareTag("Ally") && soldier.saved)
        {
            StartCoroutine(bossHit());
            health--;
            
            if (health <= 0)
            {
                spawnController.bossAlive = false;
                anim.SetBool("isDead", true);
            }
        }
    }

    IEnumerator bossHit()
    {
        anim.SetBool("isHit", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isHit", false);
    }

    IEnumerator waitForEnter()
    {
        yield return new WaitUntil(() => transform.position.x <= 8 && transform.position.x >= -8 && transform.position.z <= 5.5f && transform.position.z >= -4.25f);
        anim.SetBool("isMoving", false);
        spawnBehaviour.movementSpeed = 0f;
        Destroy(spawnBehaviour);
        arrived = true;
    }
}
