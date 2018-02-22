using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public GameObject[] enemy;
    public int numEnemies;
    public int spawnRate;


    public void StartWave(int level)
    {
        StartCoroutine(spawn(level));
    }

    IEnumerator spawn(int level)
    {
        Vector3 spawnPosition = new Vector3(0,0,0);
        Quaternion spawnRotation = new Quaternion();
        for (int i = 0; i < numEnemies; i++)
        {
            int side = Random.Range(0, 4);
            switch (side)
            {
                case 0:
                    print("top");
                    spawnPosition = new Vector3(Random.Range(-2, 2), 0, 5f);
                    break;
                case 1:
                    print("right");
                    spawnPosition = new Vector3(10, 0, Random.Range(-4, 4));
                    break;
                case 2:
                    print("left");
                    spawnPosition = new Vector3(-10, 0, Random.Range(-4, 4));
                    break;
                case 3:
                    print("bottom");
                    spawnPosition = new Vector3(Random.Range(-2, 2), 0, 5f);
                    break;
            }

            
            GameObject spawnedEnemy = Instantiate(enemy[Random.Range(0,17)], spawnPosition, spawnRotation);

            yield return new WaitForSeconds(1);
        }

    }
}
