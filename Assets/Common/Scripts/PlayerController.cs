using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public GameObject actionTriggerGameObject;

    public Collider2D actionTriggerCollider;

    private ContactFilter2D actionTriggerContactFilter = new ContactFilter2D();

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
        // TODO: Probably it would be good idea to check IsDialogInProgress. 
        // Because if cinematic started we should stop any movement and interaction. 
        // If dialog is stopped than some background NPC animations and actions may be active.
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

        if (movementDirection.x > 0) 
        {
            actionTriggerGameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
        }
        else if (movementDirection.x < 0) 
        {
            actionTriggerGameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
        }

        if (movementDirection.y > 0) 
        {
            actionTriggerGameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
        }
        else if (movementDirection.y < 0) 
        {
            actionTriggerGameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            List<Collider2D> overlappedObjects = new List<Collider2D>();
            
            var numberOfOverlappedObjects = actionTriggerCollider.OverlapCollider(actionTriggerContactFilter, overlappedObjects);

            for (int overlappedObjectIndex = 0; overlappedObjectIndex < numberOfOverlappedObjects; overlappedObjectIndex++)
            {
                var overlappedObject = overlappedObjects[overlappedObjectIndex];
                
                var actionTrigger = overlappedObject.GetComponent<ActionTrigger>();
                
                if (actionTrigger != null)
                {
                    actionTrigger.Trigger();

                    break;
                }

                var mapItem = overlappedObject.GetComponent<MapItem>();

                if (mapItem != null)
                {
                    Destroy(mapItem.gameObject);

                    break;
                }

                var mapChest = overlappedObject.GetComponent<MapChest>();

                if (mapChest != null)
                {
                    mapChest.Open();

                    break;
                }

                var mapDoor = overlappedObject.GetComponent<MapDoor>();

                if (mapDoor != null)
                {
                    SceneManager.LoadScene("Scenes/Battle");
                    
                    break;
                }
            }
        }
    }

    void FixedUpdate()
    {
        rigidbody.velocity = movementDirection * speed;
    }
}
