using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour {
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
        float appearTime = 1.2f;
        float disappearTime = 0.1f;
        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(appearTime);
            renderer.enabled = false;
            yield return new WaitForSeconds(disappearTime);
            renderer.enabled = true;
            appearTime *= 0.65f;
            disappearTime *= 0.55f;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreController.gotCoin(gameObject.transform.position.x, gameObject.transform.position.z);
            Destroy(this.gameObject);
        }
    }
}
