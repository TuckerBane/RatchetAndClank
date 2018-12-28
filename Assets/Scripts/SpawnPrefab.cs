using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour {

    public GameObject prefab;
    public bool applySpawnerRotation = false;

	// Use this for initialization
	void Awake () {
        /* maybe times transfor.rotation */
        GameObject created = Instantiate(prefab, transform.position, prefab.transform.rotation * (applySpawnerRotation ? transform.rotation : Quaternion.identity) );
        if (transform.parent)
            created.transform.parent = transform.parent;
        Destroy(gameObject);
	}
	
}
