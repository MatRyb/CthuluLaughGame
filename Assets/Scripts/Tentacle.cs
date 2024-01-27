using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Tentacle : ObjectHealth
{
    TentacleController tc;
    [SerializeField] GameObject skeletPrefab;
    bool isDestroyed = false;
    float timer = 0;
    void Start()
    {
        tc = GameObject.Find("GameManager").GetComponent<TentacleController>();
        StartHealth();     
        timer = Time.time + 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetHealth() <= 0)
        {
            isDestroyed = true;
            tc.isTentacleAlive = false;
            tc.killedTentacles += 1;
            tc.skeletonSpawns = new Transform[4];
            Destroy(gameObject, 0.3f);
        }
        
        if (!isDestroyed)
        {
            if(Time.time >= timer)
            {
                timer += 30;
                for(int i = 1; i <= 4; i++)
                {
                    GameObject t = Instantiate(skeletPrefab, tc.skeletonSpawns[i].position, Quaternion.identity);
                    t.transform.Translate(0, 1, 0);
                }
            }
        }
    }
}
