using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class TriggerIntroContinueToTheSword : MonoBehaviour
{
    public CinematicManager cinematicManager;

    public GameObject arthur;
    public GameObject lancelot;
    
    public bool finalizeWithoutCinematic;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == arthur.GetComponent<Collider2D>())
        {
            if (finalizeWithoutCinematic)
            {
                Finalize();
            }
            else
            {
                StartCoroutine(StartCinematic());    
            }
        }
    }

    private IEnumerator StartCinematic()
    {
        cinematicManager.StartCinematic();
        
        Destroy(lancelot.GetComponent<Collider2D>());
        
        var tweenerLancelot = lancelot.transform.DOMove(arthur.transform.position, Constants.SpeedRun).SetSpeedBased();
        
        yield return tweenerLancelot.WaitForCompletion();
        
        Finalize();
        
        cinematicManager.StopCinematic();
    }

    private void Finalize()
    {
        Destroy(lancelot);
        Destroy(gameObject);
    }
}
