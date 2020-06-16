using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinToWin : MonoBehaviour {

    public float spinsPerSecond = 2;
    private float currentSpin = 0.0f;
	
	// Update is called once per frame
	void Update () {
        currentSpin += (spinsPerSecond * Time.deltaTime) % 1;
        Vector3 newForward = Vector3.right.Rotate2D(currentSpin * 360.0f);
        GetComponentInParent<RotationHandler>().AddOrUpdateRotation("SpinToWin", newForward.AsRotation2d());
    }
}
