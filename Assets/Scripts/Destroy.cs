using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
