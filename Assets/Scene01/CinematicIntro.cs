using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CinematicIntro : MonoBehaviour
{
    public CinematicManager cinematicManager;
    public DialogController dialogController;

    public GameObject lamorak;
    public GameObject arthur;
    public GameObject percy;
    
    public GameObject lamorakStart;
    public GameObject arthurStart;
    public GameObject percyStart;
    
    public GameObject arthurStop;

    public GameObject lamorakStop;
    public GameObject percyStop;

    public bool finalizeWithoutCinematic;
    
    void Start()
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

    private IEnumerator StartCinematic()
    {
        cinematicManager.StartCinematic();
        
        lamorak.transform.position = lamorakStart.transform.position;
        var tweenerLamorak = lamorak.transform.DOMove(lamorakStop.transform.position, Constants.SpeedWalk).SetSpeedBased();

        arthur.transform.position = arthurStart.transform.position;
        var tweenerArthur = arthur.transform.DOMove(arthurStop.transform.position, Constants.SpeedWalk).SetSpeedBased();
        
        percy.transform.position = percyStart.transform.position;
        var tweenerPercy = percy.transform.DOMove(percyStop.transform.position, Constants.SpeedWalk).SetSpeedBased();

        yield return tweenerLamorak.WaitForCompletion();
        yield return tweenerArthur.WaitForCompletion();
        yield return tweenerPercy.WaitForCompletion();
        
        Finalize();
        
        cinematicManager.StopCinematic();
    }

    private void Finalize()
    {
        lamorak.transform.position = lamorakStop.transform.position;
        arthur.transform.position = arthurStop.transform.position;
        percy.transform.position = percyStop.transform.position;
    }
}
