using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DealWithTraitorsAsset : PlayableAsset
{
    public ExposedReference<Light> light;
    public ExposedReference<GameObject> target;
    public Color color = Color.white;
    public float intensity = 1.0f;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DealWIthTraitorsBehavior>.Create(graph);

        var lightControlBehaviour = playable.GetBehaviour();
        lightControlBehaviour.light = light.Resolve(graph.GetResolver());
        lightControlBehaviour.target = target.Resolve(graph.GetResolver());
        lightControlBehaviour.color = color;
        lightControlBehaviour.intensity = intensity;

        return playable;
    }
}
