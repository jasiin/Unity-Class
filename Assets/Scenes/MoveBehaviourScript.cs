using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class MoveBehaviourScript : MonoBehaviour
{
    public GameObject  SpnBtn;
    
    public Rigidbody2D rb;
    public float speed = 1;
    private Vector3 moveAxis;
    void Update()
    {
        moveAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveAxis = Vector3.ClampMagnitude(moveAxis, 1);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (moveAxis * speed * Time.fixedDeltaTime));
    }

}
