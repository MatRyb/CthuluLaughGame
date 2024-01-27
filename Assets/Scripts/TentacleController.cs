using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TentacleController : MonoBehaviour
{
    Transform[] tentacleSpawns = null;
    Tentacle prevTentacle = null;
    Tentacle currentTentacle = null;
    bool isTentacleAlive = false;
    bool deployTimer = false;
    float timer;
    float spawnCooldown;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isTentacleAlive && !deployTimer)
        {
            deployTimer = true; 
            timer = Time.time;
        }

        if(!isTentacleAlive && Time.time >= timer+spawnCooldown)
        {
            isTentacleAlive=true;
            deployTimer = false;


        }
    }

    Transform GeneratePosForSpawn()
    {
        //bool generatingDone = 
        while(true)
        {

        }

            return null;
    }
}
