using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour {

    public GameObject prefab;
    public GameObject effect;
    public bool applySpawnerRotation = false;
    public float spawnTimer = 3.0f;

	// Use this for initialization
	void OnEnable () {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        if (effect)
        {
            Instantiate(effect, transform.position, prefab.transform.rotation * (applySpawnerRotation ? transform.rotation : Quaternion.identity));
        }

        yield return new WaitForSeconds(spawnTimer);
        /* maybe times transfor.rotation */
        GameObject created = Instantiate(prefab, transform.position, prefab.transform.rotation * (applySpawnerRotation ? transform.rotation : Quaternion.identity));
        if (transform.parent)
            created.transform.parent = transform.parent;
        Destroy(gameObject);
        yield return null;
    }
	
}
