using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogPlayableAsset : PlayableAsset
{
    public DialogPlayableBehaviour template;
    
    public override double duration => 1.0;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<DialogPlayableBehaviour>.Create(graph, template);
    }
}
