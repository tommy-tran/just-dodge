using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public SpawnController spawner;

    void Start()
    {
        spawnWave();
    }

    void spawnWave()
    {
        spawner.StartWave(0);
    }

    void FixedUpdate()
    {
        
    }

    void GameOver()
    {

    }

}
