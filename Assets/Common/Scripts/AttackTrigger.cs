using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CircleCollider2D))]
public class AttackTrigger : MonoBehaviour
{
    public CinematicManager cinematicManager;
    
    // TODO: Refactor.
    public Collider2D target;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == target)
        {
            StartCoroutine(StartBattle());
        }
    }

    private IEnumerator StartBattle()
    {
        cinematicManager.StartCinematic();
        
        // TODO: Refactor. Hack.
        yield return gameObject.transform.parent.transform.DOMove(target.transform.position, 0.2f).WaitForCompletion();

        GameObject explosion = Instantiate(Resources.Load<GameObject>("Explosion"));
        
        // TODO: Probably implement layer sorting instead of Y sorting.
        explosion.transform.position = new Vector3(target.transform.position.x, target.transform.position.y - 1.0f, target.transform.position.z);

        // TODO: Refactor. Read real duration from animator or listen for the completion;
        yield return new WaitForSeconds(1.0f);
        
        Destroy(explosion);
        
        SceneManager.LoadScene("Scenes/Battle");  
        
        cinematicManager.StopCinematic();
    }
}
