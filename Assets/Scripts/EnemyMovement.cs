using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMovement : ObjectHealth
{
    [SerializeField] private Material[] skeleMaterials;
    [SerializeField] private Material[] skeleTrinkets;

    [SerializeField] private GameObject skeleNeck;
    [SerializeField] private GameObject skeleHead;

    [SerializeField] private GameObject spearWeapon;
    [SerializeField] private GameObject caneWeapon;

    private MoodController mCon;
    private TentacleController tCon;
    [SerializeField] public Transform attackPos;
    [SerializeField] public Transform attackPosEnd;
    [SerializeField] Animator animate;
    [SerializeField] GameObject bone;

    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public GameObject target;
    [SerializeField] private float Speed = 2;
    [SerializeField] private float AttackCooldown = 1;
    [SerializeField] private float damage = 1;
    [SerializeField] private float respawnTimer = 10.0f;
    public bool killable = true;
    public bool changedSprite = false;
    public bool neckLaceVisible = false;
    public bool headTVisible = false;
    public bool weaponUpdated = false;

    [SerializeField] AudioSource asc;
    [SerializeField] AudioClip defeat;

    [SerializeField]
    private LayerMask playerLayers;

    float attackRange = 5f;
    private bool canAttack = true;
    private bool isDead = false;

    private float removeTimer = 2000000;

    private void Start()
    { 
        target = GameObject.FindGameObjectWithTag("Player");
        mCon = GameObject.FindGameObjectWithTag("GameController").GetComponent<MoodController>();
        tCon = GameObject.FindGameObjectWithTag("GameController").GetComponent<TentacleController>();
        StartHealth();
        if(killable)
        {
            removeTimer = Time.time + UnityEngine.Random.Range(5, 10);
        }
    }

    private void Update()
    {
        if(Time.time >= removeTimer && !isDead)
        {
            isDead = true;
            tCon.currFSkeletons -= 1;
            Destroy(gameObject, 2);
        }

        if (!changedSprite && mCon.moodLevel >= 3)
        {
            changedSprite = true;
            bone.GetComponent<SkinnedMeshRenderer>().material = skeleMaterials[Random.Range(0, skeleMaterials.Length)];
        }

        if(!headTVisible && mCon.moodLevel >= 1)
        {
            skeleHead.SetActive(true);
            headTVisible = true;
            skeleHead.GetComponent<MeshRenderer>().material = skeleTrinkets[Random.Range(0, skeleTrinkets.Length)];
        }

        if (!neckLaceVisible && mCon.moodLevel >= 2)
        {
            skeleNeck.SetActive(true);
            neckLaceVisible = true;
            skeleNeck.GetComponent<MeshRenderer>().material = skeleTrinkets[Random.Range(0, skeleTrinkets.Length)];
        }
        if(!weaponUpdated && mCon.moodLevel >= 4)
        {
            spearWeapon.SetActive(false);
            caneWeapon.SetActive(true);
            weaponUpdated = true;
        }

        if (!isDead)
        {
            asc.Stop();
            asc.clip = defeat;
            asc.Play();
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
                animate.SetBool("isAttacking", true);
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
        animate.SetBool("isAttacking", false);
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
        //Color color = GetComponent<MeshRenderer>().material.color;
        isDead = true;
        animate.SetBool("isDead", true);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        //GetComponent<MeshRenderer>().material.color = Color.red;


        yield return new WaitForSeconds(respawnTimer);

        isDead = false;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
        //GetComponent<MeshRenderer>().material.color = color;
        animate.SetBool("isDead", false);
        StartHealth();
    }
}