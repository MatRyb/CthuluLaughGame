using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyMovement : ObjectHealth
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private float Speed = 2;
    private float enemyCooldown = 1;
    private float damage = 1;
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
            //GameObject.Find("Player").GetComponent<ControllerForPlayer>().currentHealth -= damage;
            StartCoroutine(AttackCooldown());
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
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
    }
}