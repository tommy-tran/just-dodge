using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : MonoBehaviour {
    public float movementSpeed;
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
