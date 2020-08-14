using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTreeTopNode : SpawnTreeMiddleNode
{
    public GameObject prefabToSpawn;
    public int itemsPerBatch = 5;
    public float timeBetweenBatches = 3;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }


    public IEnumerator SpawnRoutine()
    {
        while (true)
        {
            curSpawnIndex = ++curSpawnIndex;
            if (curSpawnIndex >= childNodes.Length)
            {
                ReadyTheDeck();
            }

            for (int i = 0; i < itemsPerBatch; ++i)
            {
                GameObject created = Instantiate(prefabToSpawn, Vector3.zero, prefabToSpawn.transform.rotation);
                Debug.Assert(created, "a thing didn't spawn properly?");
                childNodes[curSpawnIndex].Place(created);
            }
            yield return new WaitForSeconds(timeBetweenBatches);
        }
    }
}

public class BaseSpawnTreeNode : MonoBehaviour
{
    public virtual void Place(GameObject newObject) { }
}
