using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : SpawnBehaviour {

    private new Renderer renderer;

    void Start()
    {
        renderer = gameObject.GetComponentInChildren<Renderer>();
        StartCoroutine(Visibility());    
    }

    IEnumerator Visibility()
    {
        yield return new WaitForSeconds(Random.Range(0.9f, 1.1f));
        renderer.enabled = false;
        yield return new WaitForSeconds(Random.Range(0.8f, 0.9f));
        renderer.enabled = true;
        yield return StartCoroutine(Visibility());
    }
}
