using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Tentacle : ObjectHealth
{
    TentacleController tc;
    [SerializeField] GameObject skeletPrefab;
    [SerializeField] Animator animator;
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
            animator.SetBool("deadState", true);
            transform.position.Set(transform.position.x, -0.5f, transform.position.x);
            Destroy(gameObject, 1.7f);
        }
    }
}
