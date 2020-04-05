using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float Speed;

    private Animator animator;

    private int aidVelocityX;
    private int aidVelocityY;

    void Awake()
    {
        animator = GetComponent<Animator>();

        aidVelocityX = Animator.StringToHash("VelocityX");
        aidVelocityY = Animator.StringToHash("VelocityY");
    }

    // TODO: Remove isMoving.
    public void SetMovementDirection(Vector2 movementDirection, bool isMoving)
    {
        animator.SetFloat(aidVelocityX, movementDirection.x);
        animator.SetFloat(aidVelocityY, movementDirection.y);

        if (!isMoving)
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
}
