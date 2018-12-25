using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour {

    public GameObject prefab;

	// Use this for initialization
	void Awake () {
        Instantiate(prefab, transform.position, transform.rotation);
	}
	
}
