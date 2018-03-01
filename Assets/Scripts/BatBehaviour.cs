using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour {
    public SpawnBehaviour spawnBehaviour;
    new Transform transform;

    void Start()
    {
        transform = gameObject.GetComponent<Transform>();
        StartCoroutine(waitForEnter());
    }

    IEnumerator waitForEnter()
    {
        float originalSpeed = spawnBehaviour.movementSpeed;
        spawnBehaviour.movementSpeed = 1.5f;
        yield return new WaitUntil(() => transform.position.x <= 8 && transform.position.x >= -8 && transform.position.z <= 4.25 && transform.position.z >= -4.25);
        spawnBehaviour.movementSpeed = originalSpeed;
    }

}
