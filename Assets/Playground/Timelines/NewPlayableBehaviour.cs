using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

// A behaviour that is attached to a playable
[System.Serializable]
public class NewPlayableBehaviour : PlayableBehaviour
{
    //public ExposedReference<NPCController> target;
    public ExposedReference<GameObject> waypoint1;
    public ExposedReference<GameObject> waypoint2;

    // public Tween moveTween;

    // // Called when the owning graph starts playing
    // public override void OnGraphStart(Playable playable)
    // {
    //     target.transform.position = waypoint1.transform.position;
        
    //     moveTween = target.transform.DOMove(waypoint2.transform.position, target.Speed).SetSpeedBased().Pause().OnComplete(() => {         
    //         playable.SetDone(true); 
                
    //         Debug.Log("OnComplete");
    //     });

    //     playable.SetDuration(moveTween.Duration());

    //     Debug.Log(moveTween.Duration());
    // }

    // // Called when the owning graph stops playing
    // public override void OnGraphStop(Playable playable)
    // {
    //     moveTween.Kill();

    //     Debug.Log("OnGraphStop");
    // }

    // // Called when the state of the playable is set to Play
    // public override void OnBehaviourPlay(Playable playable, FrameData info)
    // {
    //     moveTween.Play();
    // }

    // // Called when the state of the playable is set to Paused
    // public override void OnBehaviourPause(Playable playable, FrameData info)
    // {
    //     moveTween.Pause();

    //     Debug.Log("OnBehaviourPause");
    // }

    // // Called each frame while the state is set to Play
    // public override void PrepareFrame(Playable playable, FrameData info)
    // {
    //     playable.SetDuration(moveTween.Duration());

    // }

    // public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    // {
        
    // }
}
