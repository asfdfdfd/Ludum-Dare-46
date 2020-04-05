using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(NewPlayableAsset))]
[TrackBindingType(typeof(NPCController))]
[System.Serializable]
public class NewTrackAsset : TrackAsset
{
    // public ExposedReference<NPCController> target;
    // public ExposedReference<GameObject> waypoint1;
    // public ExposedReference<GameObject> waypoint2;

    // // Factory method that generates a playable based on this asset
    // public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    // {
    //     var playable = ScriptPlayable<NewPlayableBehaviour>.Create(graph);

    //     var behaviour = playable.GetBehaviour();
    //     behaviour.target = target.Resolve(graph.GetResolver());
    //     behaviour.waypoint1 = waypoint1.Resolve(graph.GetResolver());
    //     behaviour.waypoint2 = waypoint2.Resolve(graph.GetResolver());

    //     return playable;
    // }

    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<NewMixerBehaviour>.Create(graph, inputCount);
    }    
}
