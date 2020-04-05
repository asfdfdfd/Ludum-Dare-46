using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class NewPlayableAsset : PlayableAsset
{
    // public ExposedReference<GameObject> waypoint1;
    // public ExposedReference<GameObject> waypoint2;

    public NewPlayableBehaviour template;

    public override double duration { get { return 0.001; } }

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<NewPlayableBehaviour>.Create(graph, template);

        // var playable = ScriptPlayable<NewPlayableBehaviour>.Create(graph);

        // var behaviour = playable.GetBehaviour();
        // behaviour.waypoint1 = waypoint1.Resolve(graph.GetResolver());
        // behaviour.waypoint2 = waypoint2.Resolve(graph.GetResolver());

        // return playable;
    } 
}
