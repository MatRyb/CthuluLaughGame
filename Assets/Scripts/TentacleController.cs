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
    float timer;
    float spawnCooldown;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isTentacleAlive)
        {

        }
    }
}
