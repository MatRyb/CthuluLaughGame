using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMovement : ObjectHealth
{
    [SerializeField] public Transform attackPos;
    [SerializeField] public Transform attackPosEnd;
    [SerializeField] Animator animate;

    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public GameObject target;
    [SerializeField] private float Speed = 2;
    [SerializeField] private float AttackCooldown = 1;
    [SerializeField] private float damage = 1;
    [SerializeField] private float respawnTimer = 10.0f;

    [SerializeField]
    private LayerMask playerLayers;

    float attackRange = 5f;
    private bool canAttack = true;
    private bool isDead = false;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        StartHealth();
        Destroy(gameObject, 30);
    }

    private void Update()
    {
        if (!isDead)
        {
            float distance = Vector3.Distance(new Vector3(target.transform.position.x, 0, target.transform.position.z),
               new Vector3(transform.position.x, 0, transform.position.z));

            if (target != null)
            {
                agent.SetDestination(target.transform.position);
                agent.speed = Speed;
            }

            if (canAttack && distance < attackRange)
            {
                transform.LookAt(target.transform);
                Collider[] hits = Physics.OverlapCapsule(new Vector3(attackPos.position.x, attackPos.position.y, attackPos.position.z),
                    new Vector3(attackPosEnd.position.x, attackPosEnd.position.y, attackPosEnd.position.z), 0.5f, playerLayers.value);

                if (hits.Length > 0)
                {
                    target.GetComponent<MovementScript>().TakeDamage(damage);
                }
                StartCoroutine(AttackSpeed());
            }
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

    public override void OnDead()
    {
         StartCoroutine(Respawner());
    }

    IEnumerator Respawner()
    {
        Color color = GetComponent<MeshRenderer>().material.color;
        isDead = true;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<MeshRenderer>().material.color = Color.red;


        yield return new WaitForSeconds(respawnTimer);

        isDead = false;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<MeshRenderer>().material.color = color;
        StartHealth();
    }
}