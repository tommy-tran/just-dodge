using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidBehaviour : MonoBehaviour {
    private new MeshRenderer renderer;
    PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        renderer = gameObject.GetComponentInChildren<MeshRenderer>();
        StartCoroutine(Visibility());
    }

    IEnumerator Visibility()
    {
        //yield return new WaitForSeconds(2f);
        float appearTime = 1.3f;
        float disappearTime = 0.15f;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(appearTime);
            renderer.enabled = false;
            yield return new WaitForSeconds(disappearTime);
            renderer.enabled = true;
            appearTime *= 0.8f;
            disappearTime *= 0.7f;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.PlayerHeal();
            Destroy(this.gameObject);
        }
    }
}
