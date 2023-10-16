using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("float")]
    [SerializeField] private float movementSpeed = 12f;
    private float horizontalMovement;

    [Header("RigidBody")]
    private Rigidbody2D rb;

    [Header("Animator")]
    private Animator animator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    

    public void LeftUp() => horizontalMovement = 0.0f;


    public void LeftDown() => horizontalMovement = -1.0f;


    public void RightUp() => horizontalMovement = 0.0f;


    public void RightDown() => horizontalMovement = 1.0f;


    private void Update()
    {
        if(horizontalMovement < 0.0f || horizontalMovement > 0.0f)
        {
            animator.CrossFade("Walk", 0.0f);
        }

        else if(horizontalMovement == 0.0f)
        {
            animator.CrossFade("Idle", 0.0f);
        }
    }


    private void FixedUpdate() => rb.velocity = new(horizontalMovement * movementSpeed, rb.velocity.y);
}