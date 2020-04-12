using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFinishedStateMachineBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPCController npcController = animator.GetComponent<NPCController>();
        
        if (npcController != null)
        {
            animator.GetComponent<NPCController>().OnAttackFinished();
        }
    }
}
