using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TentacleController : MonoBehaviour
{
    [SerializeField] private GameObject skeletPrefab;
    public int killedTentacles = 0;
    private int maxKilledTentacles = 0;

    public int freeSkeletons = 0;
    public int permaSkeletons = 1;

    private int currPSkeletons = 0;
    private int currFSkeletons = 0;

    [SerializeField] GameObject tentaclePrefab;
    GameObject player;
    [SerializeField] Transform[] tentacleSpawns = null;
    public Transform[] skeletonSpawns = new Transform[4];
    int prevTentacleIndex = -2;
    int currTentacleIndex = -1;
    public bool isTentacleAlive = false;
    bool deployTimer = false;
    float timer;
    float spawnCooldown;

    public int getKilledTentacles()
    {
        if(killedTentacles > maxKilledTentacles)
        {
            killedTentacles = maxKilledTentacles;
        }
        return killedTentacles;
    }

    void Start()
    {                                                       
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(currPSkeletons < permaSkeletons)
        {
            GameObject t = Instantiate(skeletPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            t.GetComponent<EnemyMovement>().killable = false;
            currPSkeletons++;
        }
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
            maxKilledTentacles++;
            Transform tentacleSpawn = GeneratePosForSpawn();
            GameObject t = Instantiate(tentaclePrefab, tentacleSpawn.transform.position, Quaternion.identity);
            t.transform.Translate(0, 2.0f, 0);
            skeletonSpawns = tentacleSpawn.gameObject.GetComponentsInChildren<Transform>();
        }
    }

    Transform GeneratePosForSpawn()
    {
        bool generatingDone = true;
        int rand = 0;
        while(generatingDone)
        {
            rand = UnityEngine.Random.Range(0, 7);
            if (!(rand == prevTentacleIndex) && !(rand == currTentacleIndex))
            {
                prevTentacleIndex = currTentacleIndex;
                currTentacleIndex = rand;
                generatingDone = false;
            }
        }
        return tentacleSpawns[rand];
    }

    public void UpgradeMood()
    {

    }
}
