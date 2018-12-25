using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour {

    public float lifespan = 3.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject,lifespan);
	}
}
