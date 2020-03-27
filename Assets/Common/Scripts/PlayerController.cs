using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public CinematicManager cinematicManager;

    public DialogController dialogController;

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
        // TODO: Refactor. Last second edits.
        if (cinematicManager.IsCinematicInProgress) {
            movementDirection = new Vector2(0.0f, 0.0f);
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
            return;
        }

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var dialogLines = new List<DialogLine>();
            dialogLines.Add(new DialogLine() { Name = "Test1", Message = "Test1" });
            dialogLines.Add(new DialogLine() { Name = "Test2", Message = "Test2" });

            StartCoroutine(dialogController.Show(dialogLines));
        }
    }

    void FixedUpdate()
    {
        rigidbody.velocity = movementDirection * speed;
    }
}
