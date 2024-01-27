using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyMovement : ObjectHealth
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform target;
    [SerializeField] private float Speed = 2;
    [SerializeField] private float AttackCooldown = 1;
    [SerializeField] private float damage = 1;
    private bool playerInRange = false;
    private bool canAttack = true;

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            agent.speed = Speed;
        }

        if (playerInRange && canAttack)
        {
            GameObject.Find("Player").GetComponent<MovementScript>().TakeDamage(damage);
            StartCoroutine(AttackSpeed());
            Debug.Log("Attack");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) ;
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) ;
        {
            playerInRange = false;
        }
    }
    IEnumerator AttackSpeed()
    {
        canAttack = false;
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }
}