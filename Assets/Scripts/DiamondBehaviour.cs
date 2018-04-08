using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondBehaviour : MonoBehaviour {
    private new MeshRenderer renderer;
    ScoreController scoreController;

    void Start()
    {
        scoreController = GameObject.FindObjectOfType<ScoreController>();
        renderer = gameObject.GetComponentInChildren<MeshRenderer>();
        StartCoroutine(Visibility());
    }

    IEnumerator Visibility()
    {
        //yield return new WaitForSeconds(2f);
        float appearTime = 1f;
        float disappearTime = 0.20f;
        for (int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds(appearTime);
            renderer.enabled = false;
            yield return new WaitForSeconds(disappearTime);
            renderer.enabled = true;
            appearTime *= 0.6f;
            disappearTime *= 0.5f;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreController.gotDiamond();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreController.gotDiamond();
            Destroy(this.gameObject);
        }
    }
}
