using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointForward : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 forward = GetComponent<Rigidbody>().velocity.normalized;
        GetComponent<RotationHandler>().AddOrUpdateRotation("PointForward", forward.AsRotation2d());
    }
}
