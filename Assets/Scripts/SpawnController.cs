using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public GameObject[] enemy;
    public int remaining;
    int numEnemies;
    int spawnRate;
    int enemyFloor; // Used for controlling monster spawns
    int enemyCeiling;

    void Start()
    {
        enemyFloor = 0;
    }

    public void StartWave(int level)
    {
        StartCoroutine(spawn(level));
    }

    IEnumerator spawn(int level)
    {
        remaining = level * 6 + 50;
        switch (level)
        {
            case 0:
                enemyFloor = 0;
                enemyCeiling = 0;
                break;
            case 1:
                enemyFloor = 0;
                enemyCeiling = 2;
                break;
            case 2:
                enemyFloor = 0;
                enemyCeiling = 3;
                break;
            case 3:
                enemyFloor = 0;
                enemyCeiling = 5;
                break;
            case 4:
                enemyFloor = 4;
                enemyCeiling = 7;
                break;
            case 5:
                enemyFloor = 8;
                enemyCeiling = 8;
                break;
            case 6:
                enemyFloor = 9;
                enemyCeiling = 11;
                break;
            case 7:
                enemyFloor = 0;
                enemyCeiling = 11;
                break;
            case 8:
                enemyFloor = 12;
                enemyCeiling = 13;
                break;
            case 9:
                enemyFloor = 12;
                enemyCeiling = 15;
                break;
        }

        Vector3 spawnPosition = new Vector3(0, 0, 0);
        Quaternion spawnRotation = new Quaternion();
        float randomPos;
        int side;
        GameObject currentEnemy;
        float radius;

        while (remaining > 0)
        {
            side = Random.Range(0, 4);
            currentEnemy = enemy[Random.Range(enemyFloor, enemyCeiling + 1)];
            radius = currentEnemy.transform.GetComponent<CapsuleCollider>().radius;

            // 75% Chance of spawning one enemy at a time
            if (Random.Range(0, 5) < 4)
            {
                switch (side)
                {
                    case 0:
                        spawnPosition = new Vector3(Random.Range(-8f, 8f), 0, 8);
                        spawnRotation = Quaternion.Euler(0, 180, 0);
                        break;
                    case 1:
                        spawnPosition = new Vector3(13, 0, Random.Range(-4, 4));
                        spawnRotation = Quaternion.Euler(0, 270, 0);
                        break;
                    case 2:
                        spawnPosition = new Vector3(-13, 0, Random.Range(-4, 4));
                        spawnRotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case 3:
                        spawnPosition = new Vector3(Random.Range(-8f, 8f), 0, -8f);
                        spawnRotation = Quaternion.Euler(0, 0, 0);
                        break;
                }


                Instantiate(currentEnemy, spawnPosition, spawnRotation);
                remaining--;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.6f));
                continue;

            }

            // 20% Chance of spawning more than one enemy at a time
            switch (side)
            {  
                case 0:
                    numEnemies = Mathf.RoundToInt(Mathf.Clamp(Random.Range(1f, 12f), 1, 12));
                    randomPos = Random.Range(-8f, 8f);
                    spawnPosition = new Vector3(randomPos, 0, 8);
                    spawnRotation = Quaternion.Euler(0, 180, 0);
                    if (randomPos - numEnemies * (radius + 1) > -8f || (randomPos + numEnemies * (radius + 1)) < 8f)
                    {
                        if (randomPos > 8f - (numEnemies * (radius + 1)) && randomPos - numEnemies * (radius + 1) > -8f)
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                if (Random.Range(0, 1f) <= 0.3f)
                                {
                                    continue;
                                }
                                Instantiate(currentEnemy, spawnPosition - new Vector3(i * (radius + 1f), 0, 0), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.9f));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                if (Random.Range(0, 1f) <= 0.3f)
                                {
                                    continue;
                                }
                                Instantiate(currentEnemy, spawnPosition + new Vector3(i * (radius + 1f), 0, 0), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.9f));
                            }
                        }
                    }
                    else // Spawn full line
                    {
                        spawnPosition = new Vector3(-8f, 0, 8);
                        spawnLine(side, currentEnemy, spawnPosition, spawnRotation, radius);
                    }

                    break;
                case 1:
                    numEnemies = Mathf.RoundToInt(Mathf.Clamp(Random.Range(1f, 6f), 1, 6));
                    randomPos = Random.Range(-4f, 4f);
                    spawnPosition = new Vector3(13, 0, randomPos);
                    spawnRotation = Quaternion.Euler(0, 270, 0);
                    if (randomPos - numEnemies * (radius + 1) > -4f || (randomPos + numEnemies * (radius + 1)) < 4f)
                    {
                        if (randomPos > 4f - (numEnemies * (radius + 1)) && randomPos - numEnemies * (radius + 1) > -4f)
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                if (Random.Range(0, 1f) <= 0.3f)
                                {
                                    continue;
                                }
                                Instantiate(currentEnemy, spawnPosition - new Vector3(0, 0, i * (radius + 1f)), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.9f));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                if (Random.Range(0, 1f) <= 0.3f)
                                {
                                    continue;
                                }
                                Instantiate(currentEnemy, spawnPosition + new Vector3(0, 0, i * (radius + 1f)), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.9f));
                            }
                        }
                    }
                    else // Spawn full line
                    {
                        spawnPosition = new Vector3(13f, 0, -4f);
                        spawnLine(side, currentEnemy, spawnPosition, spawnRotation, radius);
                    }
                    break;
                case 2:
                    numEnemies = Mathf.RoundToInt(Mathf.Clamp(Random.Range(1f, 6f), 1, 6));
                    randomPos = Random.Range(-4f, 4f);
                    spawnPosition = new Vector3(-13, 0, randomPos);
                    spawnRotation = Quaternion.Euler(0, 90, 0);
                    if (randomPos - numEnemies * (radius + 1) > -4f || (randomPos + numEnemies * (radius + 1)) < 4f)
                    {
                        if (randomPos > 4f - (numEnemies * (radius + 1)) && randomPos - numEnemies * (radius + 1) > -4f)
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                if (Random.Range(0, 1f) <= 0.3f)
                                {
                                    continue;
                                }
                                Instantiate(currentEnemy, spawnPosition - new Vector3(0, 0, i * (radius + 1f)), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.9f));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                if (Random.Range(0, 1f) <= 0.3f)
                                {
                                    continue;
                                }
                                Instantiate(currentEnemy, spawnPosition + new Vector3(0, 0, i * (radius + 1f)), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.9f));
                            }
                        }
                    }
                    else // Spawn full line
                    {
                        spawnPosition = new Vector3(-13f, 0, -4f);
                        spawnLine(side, currentEnemy, spawnPosition, spawnRotation, radius);
                    }
                    break;
                case 3:
                    numEnemies = Mathf.RoundToInt(Mathf.Clamp(Random.Range(1f, 12f), 1, 12));
                    randomPos = Random.Range(-8f, 8f);
                    spawnPosition = new Vector3(randomPos, 0, -8);
                    spawnRotation = Quaternion.Euler(0, 0, 0);
                    if (randomPos - numEnemies * (radius + 1) > -8f || (randomPos + numEnemies * (radius + 1)) < 8f)
                    {
                        if (randomPos > 8f - (numEnemies * (radius + 1)) && randomPos - numEnemies * (radius + 1) > -8f)
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                if (Random.Range(0, 1f) <= 0.3f)
                                {
                                    continue;
                                }
                                Instantiate(currentEnemy, spawnPosition - new Vector3(i * (radius + 1f), 0, 0), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.9f));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                if (Random.Range(0, 1f) <= 0.3f)
                                {
                                    continue;
                                }
                                Instantiate(currentEnemy, spawnPosition + new Vector3(i * (radius + 1f), 0, 0), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.9f));
                            }
                        }
                    }
                    else // Spawn full line
                    {
                        spawnPosition = new Vector3(-8f, 0, -8);
                        spawnLine(side, currentEnemy, spawnPosition, spawnRotation, radius);
                    }
                    break;
            }
            remaining -= numEnemies;
            if (numEnemies > 6)
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
            }
        }
    }

    void spawnLine(int side, GameObject currentEnemy, Vector3 spawnPosition, Quaternion spawnRotation, float radius)
    {
        float pseudoRandom = 0.2f;
        int enemiesRemoved = 0;
        for (int i = 0; i < numEnemies; i++)
        {
            if (Random.Range(0, 1f) <= pseudoRandom)
            {
                enemiesRemoved++;
                pseudoRandom /= 1.2f;
                continue;
            }
            else if ((i == numEnemies - 1) && enemiesRemoved == 0)
            {
                break;
            }
            else
            {
                switch(side)
                {
                    case 0:
                        Instantiate(currentEnemy, spawnPosition + new Vector3(i * (radius + 1f), 0, 0), spawnRotation);
                        break;
                    case 1:
                        Instantiate(currentEnemy, spawnPosition + new Vector3(0, 0, i * (radius + 1f)), spawnRotation);
                        break;
                    case 2:
                        Instantiate(currentEnemy, spawnPosition + new Vector3(0, 0, i * (radius + 1f)), spawnRotation);
                        break;
                    case 3:
                        Instantiate(currentEnemy, spawnPosition + new Vector3(i * (radius + 1f), 0, 0), spawnRotation);
                        break;
                }
                pseudoRandom *= 1.5f;
            }
        }
    }

}
