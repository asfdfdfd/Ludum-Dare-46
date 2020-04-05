using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

// https://blogs.unity3d.com/ru/2018/09/05/extending-timeline-a-practical-guide/
// https://www.codementor.io/@georgejecook/using-custom-playablebehaviours-to-create-an-event-mechanism-for-unity-s-timeline-i8ukc8wpa
// ClipEditor
// https://forum.unity.com/threads/set-playable-duration-in-code.483085/
// Probably shit. Probably not.
// https://forum.unity.com/threads/timeline-track-for-character-movement.659641/
// https://www.youtube.com/watch?v=Y5RDtN1jM6A — probably has good sample.
// https://forum.unity.com/threads/workflow-for-cutscenes-with-pauses.515187/

// Try to google "Unity Timeline fixed character movement speed"

// A behaviour that is attached to a playable
public class NewMixerBehaviour : PlayableBehaviour
{
    // private Tween moveTween;

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        // target.transform.position = waypoint1.transform.position;
        
        // moveTween = target.transform.DOMove(waypoint2.transform.position, target.Speed).SetSpeedBased().Pause().OnComplete(() => {         
        //     playable.SetDone(true); 
                
        //     Debug.Log("OnComplete");
        // });

        // playable.SetDuration(moveTween.Duration());

        // Debug.Log(moveTween.Duration());
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        // moveTween.Kill();

        // Debug.Log("OnGraphStop");
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        // moveTween.Play();

        // Debug.Log("OnBehaviourPlay");
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        // moveTween.Pause();

        // Debug.Log("OnBehaviourPause");
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        // playable.SetDuration(moveTween.Duration());

    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var controller = playerData as NPCController;

        if (controller == null) 
        {
            return;
        }
    }
}
