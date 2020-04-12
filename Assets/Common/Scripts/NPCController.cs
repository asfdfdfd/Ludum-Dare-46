using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private static readonly int LAYER_IDLE = 0;
    private static readonly int LAYER_WALK = 1;
    private static readonly int LAYER_ATTACK = 2;
        
    public float speed;

    private Animator _animator;

    private int _aidVelocityX;
    private int _aidVelocityY;

    private int _aidIsAttacking;

    void Awake()
    {
        _animator = GetComponent<Animator>();

        _aidVelocityX = Animator.StringToHash("VelocityX");
        _aidVelocityY = Animator.StringToHash("VelocityY");

        _aidIsAttacking = Animator.StringToHash("IsAttacking");
    }

    // TODO: Remove isMoving.
    public void SetMovementDirection(Vector2 movementDirection, bool isMoving)
    {
        _animator.SetFloat(_aidVelocityX, movementDirection.x);
        _animator.SetFloat(_aidVelocityY, movementDirection.y);

        if (!isMoving)
        {
            _animator.SetLayerWeight(LAYER_IDLE, 1.0f);
            _animator.SetLayerWeight(LAYER_WALK, 0.0f);
            _animator.SetLayerWeight(LAYER_ATTACK, 0.0f);
        }
        else
        {
            _animator.SetLayerWeight(LAYER_IDLE, 0.0f);
            _animator.SetLayerWeight(LAYER_WALK, 1.0f);
            _animator.SetLayerWeight(LAYER_ATTACK, 0.0f);
        }        
    }

    public void Attack()
    {
        _animator.SetFloat(_aidVelocityX, 0.0f);
        _animator.SetFloat(_aidVelocityY, -1.0f);
        
        _animator.SetLayerWeight(LAYER_IDLE, 0.0f);
        _animator.SetLayerWeight(LAYER_WALK, 0.0f);
        _animator.SetLayerWeight(LAYER_ATTACK, 1.0f);
        
        _animator.SetBool(_aidIsAttacking, true);
    }
}
