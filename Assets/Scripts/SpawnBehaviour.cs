using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : MonoBehaviour {
    public float movementSpeed;
    ScoreController scoreController;
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
	}

    void OnTriggerEnter(Collider other)
    {
        scoreController = GameObject.FindObjectOfType<ScoreController>();
        if (other.CompareTag("Boundary"))
        {
            scoreController.dodgedEnemy();
            Destroy(this.gameObject);
        }
    }
}
