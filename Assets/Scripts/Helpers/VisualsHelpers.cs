using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VisualsHelpers
{
    public static GameObject AddFloatingIconToObject(GameObject iconArchetype, GameObject target, Vector3 extraOffset = default)
    {
        GameObject visualEffect = GameObject.Instantiate(iconArchetype);
        Vector3 position = visualEffect.transform.position;
        if (!visualEffect.GetComponent<PositionOnlyParent>())
            visualEffect.AddComponent<PositionOnlyParent>();

        visualEffect.GetComponent<PositionOnlyParent>().parent = target;
        visualEffect.GetComponent<PositionOnlyParent>().localPosition = position + extraOffset;

        return visualEffect;
    }

}
