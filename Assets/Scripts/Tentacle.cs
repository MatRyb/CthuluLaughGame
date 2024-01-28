using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Tentacle : ObjectHealth
{
    TentacleController tc;
    [SerializeField] GameObject skeletPrefab;
    void Start()
    {
        tc = GameObject.Find("GameManager").GetComponent<TentacleController>();
        StartHealth();     
    }

    void Update()
    {
        if(GetHealth() <= 0)
        {
            tc.isTentacleAlive = false;
            tc.killedTentacles += 1;
            tc.skeletonSpawns = new Transform[4];
            Destroy(gameObject, 0.3f);
        }
    }
}
