using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMovement : ObjectHealth
{
    [SerializeField] public Transform attackPos;
    [SerializeField] public Transform attackPosEnd;

    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform target;
    [SerializeField] private float Speed = 2;
    [SerializeField] private float AttackCooldown = 1;
    [SerializeField] private float damage = 1;

    [SerializeField]
    private LayerMask playerLayers;

    float attackRange = 5f;
    private bool canAttack = true;

    private void Start()
    {
        StartHealth();
    }

    private void Update()
    {
        float distance = Vector3.Distance(new Vector3(target.position.x, 0, target.position.z),
               new Vector3(transform.position.x, 0, transform.position.z));

        if (target != null)
        {
            agent.SetDestination(target.position);
            agent.speed = Speed;
        }

        if (canAttack && distance < attackRange)
        {
            Transform trasforms = transform;
            trasforms.LookAt(target);
            Collider[] hits = Physics.OverlapCapsule(new Vector3(attackPos.position.x, attackPos.position.y, attackPos.position.z), 
                new Vector3(attackPosEnd.position.x, attackPosEnd.position.y, attackPosEnd.position.z), 0.5f, playerLayers.value);

            if (hits.Length > 0)
            {
                target.GetComponent<MovementScript>().TakeDamage(damage);
            }
            StartCoroutine(AttackSpeed());
            Debug.Log("Attack");
        }
    }
    IEnumerator AttackSpeed()
    {
        canAttack = false;
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;   
        Gizmos.DrawWireSphere(new Vector3(attackPos.position.x, attackPos.position.y, attackPos.position.z), 0.5f);
        Gizmos.DrawWireSphere(new Vector3(attackPosEnd.position.x, attackPosEnd.position.y, attackPosEnd.position.z), 0.5f);
    }
}