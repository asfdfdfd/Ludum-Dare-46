using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    private Animator animator;

    public float speed = 0.0f;

    private Vector2 movementDirection;

    private int aidVelocityX;
    private int aidVelocityY;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        aidVelocityX = Animator.StringToHash("VelocityX");
        aidVelocityY = Animator.StringToHash("VelocityY");
    }

    void Update()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        animator.SetFloat(aidVelocityX, movementDirection.x);
        animator.SetFloat(aidVelocityY, movementDirection.y);

        if (movementDirection.x == 0 && movementDirection.y == 0)
        {
            animator.SetLayerWeight(0, 1.0f);
            animator.SetLayerWeight(1, 0.0f);
        }
        else
        {
            animator.SetLayerWeight(0, 0.0f);
            animator.SetLayerWeight(1, 1.0f);
        }      
    }

    void FixedUpdate()
    {
        rigidbody.velocity = movementDirection * speed;
    }
}
