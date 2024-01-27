using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TentacleController : MonoBehaviour
{
    [SerializeField] GameObject tentaclePrefab;
    GameObject player;
    [SerializeField] Transform[] tentacleSpawns = null;
    int prevTentacleIndex = -2;
    int currTentacleIndex = -1;
    Tentacle currentTentacle = null;
    public bool isTentacleAlive = false;
    bool deployTimer = false;
    float timer;
    float spawnCooldown;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTentacleAlive && !deployTimer)
        {
            deployTimer = true; 
            timer = Time.time;
            spawnCooldown = UnityEngine.Random.Range(3, 8); 
        }

        if(!isTentacleAlive && Time.time >= timer+spawnCooldown && deployTimer)
        {
            isTentacleAlive = true;
            deployTimer = false;
            Transform tentacleSpawn = GeneratePosForSpawn();
            GameObject t = Instantiate(tentaclePrefab, tentacleSpawn.transform.position, Quaternion.identity);
            t.transform.Translate(0, 2.0f, 0);
        }
    }

    Transform GeneratePosForSpawn()
    {
        bool generatingDone = true;
        int rand = 0;
        while(generatingDone)
        {
            rand = UnityEngine.Random.Range(0, 7);
            if (!(rand == prevTentacleIndex) || !(rand == currTentacleIndex))
            {
                prevTentacleIndex = currTentacleIndex;
                currTentacleIndex = rand;
                generatingDone = false;
            }
        }
        return tentacleSpawns[rand];
    }
}
