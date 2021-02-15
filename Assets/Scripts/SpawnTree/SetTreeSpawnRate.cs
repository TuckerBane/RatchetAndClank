using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTreeSpawnRate : MonoBehaviour
{
    public SpawnTreeTopNode target;
    public float newTimeBetweenBatches = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(target != null, "Needs a target");
        target.timeBetweenBatches = newTimeBetweenBatches;
    }
}
