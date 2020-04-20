using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CinematicIntro : MonoBehaviour
{
    public CinematicManager cinematicManager;
    public DialogController dialogController;
    
    public GameObject arthur;
    public GameObject lancelot;
    
    public GameObject arthurStart;
    public GameObject lancelotStart;
    
    public GameObject arthurStop;
    public GameObject lancelotStop;

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
        
        arthur.transform.position = arthurStart.transform.position;
        var tweenerArthur = arthur.transform.DOMove(arthurStop.transform.position, Constants.SpeedWalk).SetSpeedBased();
        
        lancelot.transform.position = lancelotStart.transform.position;
        var tweenerLancelot = lancelot.transform.DOMove(lancelotStop.transform.position, Constants.SpeedWalk).SetSpeedBased();
        
        yield return tweenerArthur.WaitForCompletion();
        yield return tweenerLancelot.WaitForCompletion();

        yield return dialogController.Show(DialogLineAvatar.Lancelot, "Lancelot", "Arthur?");
        yield return dialogController.Show(DialogLineAvatar.Arthur, "Arthur", "Do you feel it?");
        yield return dialogController.Show(DialogLineAvatar.Lancelot, "Lancelot", "What?");
        yield return dialogController.Show(DialogLineAvatar.Arthur, "Arthur", "Power of the sword. It's getting stronger.");
        yield return dialogController.Show(DialogLineAvatar.Arthur, "Arthur", "Let's go.");

        yield return lancelot.transform.DOMove(arthur.transform.position, Constants.SpeedWalk).SetSpeedBased()
            .WaitForCompletion();
        
        Finalize();
        
        cinematicManager.StopCinematic();
    }

    private void Finalize()
    {
        arthur.transform.position = arthurStop.transform.position;
        Destroy(lancelot);
    }
}
