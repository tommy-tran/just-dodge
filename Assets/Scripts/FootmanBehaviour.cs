using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootmanBehaviour : MonoBehaviour {
    public SpawnBehaviour spawnBehaviour;
    public bool saved;
    new Transform transform;
    Animator anim;
    

    void Start()
    {
        transform = gameObject.GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isMoving", true);
        saved = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            saved = true;
            transform.LookAt(GameObject.FindGameObjectWithTag("Boss").transform);
        }

        if (other.CompareTag("Boss") && saved) 
        {
            spawnBehaviour.movementSpeed = 0;
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
