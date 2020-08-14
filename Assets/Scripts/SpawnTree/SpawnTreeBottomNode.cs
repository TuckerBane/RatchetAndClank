using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is designed to work for 2d areas with z always being 0
public class SpawnTreeBottomNode : BaseSpawnTreeNode
{
    public override void Place(GameObject newObject)
    {
        Debug.Assert(transform.lossyScale.x >= newObject.transform.lossyScale.x, "Object " + newObject.name + " is bigger then the zone <" + name + ">, where it's supposed to be spawned");
        Debug.Assert(transform.lossyScale.y >= newObject.transform.lossyScale.y, "Object " + newObject.name + " is bigger then the zone <" + name + ">, where it's supposed to be spawned");

        Vector3 spawnBox = (transform.lossyScale - newObject.transform.lossyScale) / 2.0f;
        newObject.transform.position = transform.position + new Vector3(Random.Range(-spawnBox.x, spawnBox.x), Random.Range(-spawnBox.y, spawnBox.y), 0);
    }
}
