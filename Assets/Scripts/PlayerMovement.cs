    using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public Vector3 movement;

   /* public void OnLook(InputValue value)
    {
        m_Look = value.Get<Vector2>();
    }*/

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //transform.rotation *= Quaternion.AngleAxis()
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        Move(movement);
    }

    void Move(Vector3 dir)
    {
        rb.velocity = dir * speed;
    }

}
