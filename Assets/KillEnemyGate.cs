using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyGate : MonoBehaviour {

    public GameObject spawner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (spawner.transform.childCount == 0 )
            Destroy(gameObject);
	}
}
