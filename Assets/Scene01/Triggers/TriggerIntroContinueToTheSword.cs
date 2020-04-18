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
    public GameObject lamorak;
    public GameObject percy;
    
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

        Destroy(lamorak.GetComponent<Collider2D>());
        Destroy(percy.GetComponent<Collider2D>());
        
        var tweenerLamorak = lamorak.transform.DOMove(arthur.transform.position, Constants.SpeedRun).SetSpeedBased();
        var tweenerPercy = percy.transform.DOMove(arthur.transform.position, Constants.SpeedRun).SetSpeedBased();

        yield return tweenerLamorak.WaitForCompletion();
        yield return tweenerPercy.WaitForCompletion();
        
        Finalize();
        
        cinematicManager.StopCinematic();
    }

    private void Finalize()
    {
        Destroy(lamorak);
        Destroy(percy);
        Destroy(gameObject);
    }
}
