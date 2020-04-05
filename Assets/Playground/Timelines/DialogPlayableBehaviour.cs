using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class DialogPlayableBehaviour : PlayableBehaviour
{
    public ExposedReference<MonoBehaviour> monoBehaviourReference;
    public ExposedReference<DialogController> dialogControllerReference;
    
    public string title;
    public string message;

    private MonoBehaviour _monoBehaviour;
    private DialogController _dialogController;
    private PlayableDirector _playableDirector;
    
    public override void OnPlayableCreate(Playable playable)
    {
        _playableDirector = playable.GetGraph().GetResolver() as PlayableDirector;
        _dialogController = dialogControllerReference.Resolve(_playableDirector);
        _monoBehaviour = monoBehaviourReference.Resolve(_playableDirector);
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);

        _monoBehaviour.StartCoroutine(ShowDialogAndAwaitUserInput(playable));
    }

    private IEnumerator ShowDialogAndAwaitUserInput(Playable playable)
    {
        _playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);

        yield return _dialogController.Show(title, message);

        // TODO: Last dialog item displays twice. Probably cumulative time is wrong.
        // Probably should add reference to exact start timing.
        // Something like this https://forum.unity.com/threads/how-to-access-the-clip-timing-start-end-time-in-playablebehaviour-functions.494344/
        _playableDirector.time += playable.GetDuration();
        
        _playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
