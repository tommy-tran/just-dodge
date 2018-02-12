using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject enemy;
    public Vector3 spawnHorizontal;
    public int numEnemies;

    void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            int side = Random.Range(0, 2);
            print(side);
            Vector3 spawnPosition;
            if (side == 0)
            {
                spawnPosition = new Vector3(-9, 0, Random.Range(-spawnHorizontal.z, spawnHorizontal.z));
            }
            else
            {
                spawnPosition = new Vector3(9, 0, Random.Range(-spawnHorizontal.z, spawnHorizontal.z));
            }
            Quaternion spawnRotation = new Quaternion();
            GameObject spawnedEnemy = Instantiate(enemy, spawnPosition, spawnRotation);
            
            yield return new WaitForSeconds(3);
        }

    }
}
