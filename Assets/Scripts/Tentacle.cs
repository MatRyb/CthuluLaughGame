using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : ObjectHealth
{
    TentacleController tc;
    void Start()
    {
        tc = GameObject.Find("GameManager").GetComponent<TentacleController>();
        StartHealth();       
    }

    // Update is called once per frame
    void Update()
    {
        if(GetHealth() <= 0)
        {
            tc.isTentacleAlive = false;
            Destroy(gameObject, 0.3f);
        }
    }
}
