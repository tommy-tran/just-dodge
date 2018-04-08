using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBehaviour : MonoBehaviour {
    private new SpriteRenderer renderer;
    PlayerController playerController;
    bool danger;
    

    void Start()
    {
        danger = false;
        playerController = GameObject.FindObjectOfType<PlayerController>();
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(Visibility());
    }

    IEnumerator Visibility()
    {
        float appearTime = 0.7f;
        float disappearTime = 0.15f;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(appearTime);
            renderer.enabled = false;
            yield return new WaitForSeconds(disappearTime);
            renderer.enabled = true;
            appearTime *= 0.6f;
            disappearTime *= 0.6f;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && danger && playerController.isAlive && !playerController.invulnerable)
        {
            playerController.PlayerDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && danger && playerController.isAlive && !playerController.invulnerable)
        {
            playerController.PlayerDamage();
        }
    }
}
