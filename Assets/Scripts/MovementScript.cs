using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MovementScript : ObjectHealth
{
    [SerializeField] GameObject restartScreen;
    public CharacterController controller;
    [SerializeField] Transform attackTarget;
    public Transform cam;
    public float speed = 6f;
    [SerializeField] float dmg;
    public Slider slider;

    float timer;

    [SerializeField] private float attackDuration = 1.0f;

    bool isAttacking = false;

    [SerializeField]
    private LayerMask enemyLayers;

    public float turnLerpTime = 0.1f;
    float turnSmoothVelocity;
    void Start()
    {
        StartHealth();
        Cursor.lockState = CursorLockMode.Locked;
        controller.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.SimpleMove(Vector3.zero);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;

        if(dir.magnitude >= 0.1f )
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.rotation.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnLerpTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if(isAttacking)
            if(Time.time >= timer + attackDuration)
                isAttacking = false;

        if(Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            Attack();
            Debug.Log("Boop");
        }
    }

    private void Attack()
    {
        timer = Time.time;
        isAttacking = true;
        Collider[] hits = Physics.OverlapCapsule(new Vector3(attackTarget.position.x, attackTarget.position.y + 0.2f, attackTarget.position.z),
            new Vector3(attackTarget.position.x, attackTarget.position.y - 0.2f, attackTarget.position.z), 0.75f, enemyLayers.value);
    
        for(int i=0; i<hits.Length; i++)
        {
            hits[i].gameObject.GetComponent<ObjectHealth>().TakeDamage(dmg);
        }
    }

    public override void TakeDamage(float value)
    {
        base.TakeDamage(value);
        slider.value = GetHealth();
    }

    public override void OnDead()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0.0f;
        restartScreen.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector3(attackTarget.position.x, attackTarget.position.y+0.2f, attackTarget.position.z), 0.75f);
        Gizmos.DrawWireSphere(new Vector3(attackTarget.position.x, attackTarget.position.y, attackTarget.position.z), 0.75f);
        Gizmos.DrawWireSphere(new Vector3(attackTarget.position.x, attackTarget.position.y-0.2f, attackTarget.position.z), 0.75f);
    }

    public void OnRestartBtnClick()
    {

    }
}
