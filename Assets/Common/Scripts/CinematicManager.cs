using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    private bool isCinematicInProgress = false;

    public bool IsCinematicInProgress
    {
        get
        {
            return isCinematicInProgress;
        }
    }

    public void StartCinematic()
    {
        isCinematicInProgress = true;
    }

    public void StopCinematic()
    {
        isCinematicInProgress = false;
    }
}
