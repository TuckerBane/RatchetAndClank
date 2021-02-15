using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class DealWIthTraitorsBehavior : PlayableBehaviour
{
    public Light light = null;
    public GameObject target = null;
    public Color color = Color.white;
    public float intensity = 1f;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (light != null)
        {
            light.color = color;
            light.intensity = intensity;
        }

        if (target != null)
        {
            GameObject.Destroy(target);
        }
    }
}
