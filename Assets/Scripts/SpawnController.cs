using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public GameObject[] enemy;
    public int numEnemies;
    public int spawnRate;
    public int remaining;

    public void StartWave(int level)
    {
        StartCoroutine(spawn(level));
    }

    IEnumerator spawn(int level)
    {
        remaining = level * 10 + 30;
        
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        Quaternion spawnRotation = new Quaternion();
        float randomPos;

        while (remaining > 0)
        {
            int side = Random.Range(0, 4);
            GameObject currentEnemy = enemy[Random.Range(0, 16)];
            float radius = currentEnemy.transform.GetComponent<CapsuleCollider>().radius;

            // 75% Chance of spawning one enemy at a time
            if (Random.Range(0, 4) < 3)
            {
                switch (side)
                {
                    case 0:
                        spawnPosition = new Vector3(Random.Range(-8f, 8f), 0, 6);
                        spawnRotation = Quaternion.Euler(0, 180, 0);
                        break;
                    case 1:
                        spawnPosition = new Vector3(11, 0, Random.Range(-4, 4));
                        spawnRotation = Quaternion.Euler(0, 270, 0);
                        break;
                    case 2:
                        spawnPosition = new Vector3(-11, 0, Random.Range(-4, 4));
                        spawnRotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case 3:
                        spawnPosition = new Vector3(Random.Range(-8f, 8f), 0, -6f);
                        spawnRotation = Quaternion.Euler(0, 0, 0);
                        break;
                }


                Instantiate(enemy[Random.Range(0, 16)], spawnPosition, spawnRotation);
                remaining--;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
                continue;

            }

            // 25% Chance of spawning more than one enemy at a time
            switch (side)
            {
                case 0:
                    numEnemies = Mathf.RoundToInt(Mathf.Clamp(Random.Range(1f, 12f), 1, 12));
                    randomPos = Random.Range(-8f, 8f);
                    spawnPosition = new Vector3(randomPos, 0, 6);
                    spawnRotation = Quaternion.Euler(0, 180, 0);
                    if (randomPos - numEnemies * (radius + 1) > -8f || (randomPos + numEnemies * (radius + 1)) < 8f)
                    {
                        if (randomPos > 8f - (numEnemies * (radius + 1)) && randomPos - numEnemies * (radius + 1) > -8f)
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                Instantiate(currentEnemy, spawnPosition - new Vector3(i * (radius + 1f), 0, 0), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.2f, 1f));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                Instantiate(currentEnemy, spawnPosition + new Vector3(i * (radius + 1f), 0, 0), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.2f, 1f));
                            }
                        }
                    }
                    else // Spawn full line
                    {
                        spawnPosition = new Vector3(-8f, 0, 6);
                        int remove = Random.Range(1, 4); // Number of enemies to remove
                        float pseudoRandom = 1 / 12f;
                        int enemiesRemoved = 0;

                        for (int i = 0; i < numEnemies; i++)
                        {
                            if (Random.Range(0, 1f) <= pseudoRandom)
                            {
                                enemiesRemoved++;
                                pseudoRandom /= 1.4f;
                                continue;
                            }
                            else if ((i == numEnemies - 1) && enemiesRemoved == 0)
                            {
                                break;
                            }
                            else
                            {
                                Instantiate(currentEnemy, spawnPosition + new Vector3(i * (radius + 1f), 0, 0), spawnRotation);
                                pseudoRandom *= 1.2f;
                            }
                        }
                    }

                    break;
                case 1:
                    numEnemies = Mathf.RoundToInt(Mathf.Clamp(Random.Range(1f, 6f), 1, 6));
                    randomPos = Random.Range(-4f, 4f);
                    spawnPosition = new Vector3(11, 0, randomPos);
                    spawnRotation = Quaternion.Euler(0, 270, 0);
                    if (randomPos - numEnemies * (radius + 1) > -4f || (randomPos + numEnemies * (radius + 1)) < 4f)
                    {
                        if (randomPos > 4f - (numEnemies * (radius + 1)) && randomPos - numEnemies * (radius + 1) > -4f)
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                Instantiate(currentEnemy, spawnPosition - new Vector3(0, 0, i * (radius + 1f)), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.2f, 1f));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                Instantiate(currentEnemy, spawnPosition + new Vector3(0, 0, i * (radius + 1f)), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.2f, 1f));
                            }
                        }
                    }
                    else // Spawn full line
                    {
                        spawnPosition = new Vector3(11f, 0, -4f);
                        float pseudoRandom = 1 / 6f;
                        int enemiesRemoved = 0;

                        for (int i = 0; i < numEnemies; i++)
                        {
                            if (Random.Range(0, 1f) <= pseudoRandom)
                            {
                                enemiesRemoved++;
                                pseudoRandom /= 1.4f;
                                continue;
                            }
                            else if ((i == numEnemies - 1) && enemiesRemoved == 0)
                            {
                                break;
                            }
                            else
                            {
                                Instantiate(currentEnemy, spawnPosition + new Vector3(0, 0, i * (radius + 1f)), spawnRotation);
                                pseudoRandom *= 1.2f;
                            }
                        }
                    }
                    break;
                case 2:
                    numEnemies = Mathf.RoundToInt(Mathf.Clamp(Random.Range(1f, 6f), 1, 6));
                    randomPos = Random.Range(-4f, 4f);
                    spawnPosition = new Vector3(-11, 0, randomPos);
                    spawnRotation = Quaternion.Euler(0, 90, 0);
                    if (randomPos - numEnemies * (radius + 1) > -4f || (randomPos + numEnemies * (radius + 1)) < 4f)
                    {
                        if (randomPos > 4f - (numEnemies * (radius + 1)) && randomPos - numEnemies * (radius + 1) > -4f)
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                Instantiate(currentEnemy, spawnPosition - new Vector3(0, 0, i * (radius + 1f)), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.2f, 1f));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                Instantiate(currentEnemy, spawnPosition + new Vector3(0, 0, i * (radius + 1f)), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.2f, 1f));
                            }
                        }
                    }
                    else // Spawn full line
                    {
                        spawnPosition = new Vector3(-11f, 0, -4f);
                        float pseudoRandom = 1 / 12f;
                        int enemiesRemoved = 0;

                        for (int i = 0; i < numEnemies; i++)
                        {
                            if (Random.Range(0, 1f) <= pseudoRandom)
                            {
                                enemiesRemoved++;
                                pseudoRandom /= 1.4f;
                                continue;
                            }
                            else if ((i == numEnemies - 1) && enemiesRemoved == 0)
                            {
                                break;
                            }
                            else
                            {
                                Instantiate(currentEnemy, spawnPosition + new Vector3(0, 0, i * (radius + 1f)), spawnRotation);
                                pseudoRandom *= 1.2f;
                            }
                        }
                    }
                    break;
                case 3:
                    numEnemies = Mathf.RoundToInt(Mathf.Clamp(Random.Range(1f, 12f), 1, 12));
                    randomPos = Random.Range(-8f, 8f);
                    spawnPosition = new Vector3(randomPos, 0, -6);
                    spawnRotation = Quaternion.Euler(0, 0, 0);
                    if (randomPos - numEnemies * (radius + 1) > -8f || (randomPos + numEnemies * (radius + 1)) < 8f)
                    {
                        if (randomPos > 8f - (numEnemies * (radius + 1)) && randomPos - numEnemies * (radius + 1) > -8f)
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                Instantiate(currentEnemy, spawnPosition - new Vector3(i * (radius + 1f), 0, 0), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.2f, 1f));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < numEnemies; i++)
                            {
                                Instantiate(currentEnemy, spawnPosition + new Vector3(i * (radius + 1f), 0, 0), spawnRotation);
                                yield return new WaitForSeconds(Random.Range(0.2f, 1f));
                        }
                        }
                    }
                    else // Spawn full line
                    {
                        spawnPosition = new Vector3(-8f, 0, -6);
                        int remove = Random.Range(1, 4); // Number of enemies to remove
                        float pseudoRandom = 1 / 12f;
                        int enemiesRemoved = 0;

                        for (int i = 0; i < numEnemies; i++)
                        {
                            if (Random.Range(0, 1f) <= pseudoRandom)
                            {
                                enemiesRemoved++;
                                pseudoRandom /= 1.4f;
                                continue;
                            }
                            else if ((i == numEnemies - 1) && enemiesRemoved == 0)
                            {
                                break;
                            }
                            else
                            {
                                Instantiate(currentEnemy, spawnPosition + new Vector3(i * (radius + 1f), 0, 0), spawnRotation);
                                pseudoRandom *= 1.2f;
                            }
                        }
                    }
                    break;
            }
            remaining -= numEnemies;
            if (numEnemies > 6)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 3f));
            }
        }
    }

}
