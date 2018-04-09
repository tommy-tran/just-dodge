using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public GameObject[] enemy;
    public int remaining;
    public bool bossAlive;
    public GameObject ally;
    public GameObject bigBoss;
    public GameObject bossMinions;
    public GameObject dangerCircle;
    public GameObject coin;
    public GameObject diamond;
    public GameObject firstAid;
    public DialogueTrigger soldierDialogue;
    Coroutine coinCreator;
    Coroutine circleCreator;
    int numEnemies;
    int spawnRate;
    int enemyFloor;
    int enemyCeiling;
    int spawnLimiter;

    void Start()
    {
        enemyFloor = 0;
        bossAlive = true;
        spawnLimiter = 0;
    }

    public void StartWave(int level)
    {
        spawnLimiter = 5 + level;
        coinCreator = StartCoroutine(createCoin());
        if (level > 6) { circleCreator = StartCoroutine(createCircle()); }
        if (level == 2) { StartCoroutine(createDiamond()); }
        StartCoroutine(spawn(level));
    }

    IEnumerator spawn(int level)
    {
        remaining = level * 10 + 50;
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
            case 10:
                enemyFloor = 16;
                enemyCeiling = 17;
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

            // 90% Chance of spawning one enemy at a time
            if (Random.Range(0, 15) < 14)
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
                yield return new WaitForSeconds(Random.Range(0.1f - (level * 0.003f), 1f - (level * 0.03f)));
                continue;
            }

            // 20% Chance of spawning more than one enemy at a time
            switch (side)
            {  
                case 0:
                    numEnemies = Random.Range(1, 6);
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
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
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
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
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
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
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
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
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
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
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
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
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
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
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
                                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
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
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length <= spawnLimiter);
        }
        StopCoroutine(coinCreator);
        if (circleCreator != null)StopCoroutine(circleCreator);
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


    IEnumerator bossWave(BossBehaviour behaviour)
    {
        int side;
        float radius;
        float min = 0.008f;
        float max = 0.08f;
        int multiplier = 0;

        yield return new WaitUntil(() => behaviour.arrived);
        yield return new WaitForSeconds(3f);

        circleCreator = StartCoroutine(createCircle());
        coinCreator = StartCoroutine(createCoin());
        remaining = Random.Range(20, 30);

        Vector3 spawnPosition = new Vector3(0, 0, 0);
        Quaternion spawnRotation = new Quaternion();


        while (bossAlive)
        {
            side = Random.Range(0, 4);
            radius = bossMinions.transform.GetComponent<CapsuleCollider>().radius;
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

            remaining--;
            if (remaining <= 0)
            {
                if (multiplier < 9)
                {
                    if (multiplier == 0)
                    {
                        StartCoroutine(soldierAction());
                    }
                    multiplier++;

                } else if (multiplier % 3 == 0)
                {
                    StartCoroutine(createCircle());
                }
                remaining = Random.Range(35, 70);
                Instantiate(ally, spawnPosition, spawnRotation);
            } else
            {
                Instantiate(bossMinions, spawnPosition, spawnRotation);
            }
            yield return new WaitForSeconds(Random.Range(0.1f - (min * multiplier), 1f - (max * multiplier)));
        }
        StopCoroutine(createCircle());
    }

    IEnumerator soldierAction()
    {
        yield return new WaitForSeconds(0.1f);
        soldierDialogue.TriggerDialog(false);
    }

    public void spawnBoss()
    {
        GameObject boss = Instantiate(bigBoss);
        BossBehaviour bossBehaviour = boss.GetComponent<BossBehaviour>();
        StartCoroutine(bossWave(bossBehaviour));
        StartCoroutine(bossDeath());
    }

    IEnumerator createCircle()
    {
        float radius = dangerCircle.transform.GetComponent<SphereCollider>().radius;
        
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 8));
            Instantiate(dangerCircle, new Vector3(Random.Range(-8f + radius, 8f - radius), 0, Random.Range(-4.25f + radius, 4.25f - radius)), Quaternion.Euler(90, 0, 0));
        }
    }

    IEnumerator createCoin()
    {
        float radius = coin.transform.GetComponent<SphereCollider>().radius;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 13));
            Instantiate(coin, new Vector3(Random.Range(-8f + radius, 8f - radius), 0.5f, Random.Range(-4.25f + radius, 4.25f - radius)), Quaternion.Euler(90, 0, 0));
        }
    }

    IEnumerator createDiamond()
    {
        float radius = coin.transform.GetComponent<SphereCollider>().radius;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15, 30));
            Instantiate(diamond, new Vector3(Random.Range(-8f + radius, 8f - radius), 0.5f, Random.Range(-4.25f + radius, 4.25f - radius)), Quaternion.Euler(55, 0, 0));
        }
    }

    IEnumerator bossDeath()
    {
        yield return new WaitUntil(() => !bossAlive);
        StopCoroutine(circleCreator);
        StopAllCoroutines();
        StopCoroutine(coinCreator);
    }

    public void spawnHealth()
    {
        float radius = 0.8f;
        Instantiate(firstAid, new Vector3(Random.Range(-8f + radius, 8f - radius), 0.5f, Random.Range(-4.25f + radius, 4.25f - radius)), Quaternion.Euler(0, 0, 0));
    }
}
