using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Tentacle : ObjectHealth
{
    TentacleController tc;
    [SerializeField] GameObject skeletPrefab;
    [SerializeField] AudioClip clip;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    private float prevHp;
    void Start()
    {
        tc = GameObject.Find("GameManager").GetComponent<TentacleController>();
        StartHealth();
        prevHp = GetHealth();
    }

    void Update()
    {
        if(prevHp > GetHealth() )
        {
            audioSource.clip = clip;
            audioSource.Stop();
            audioSource.Play();
            prevHp = GetHealth();
        }

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
