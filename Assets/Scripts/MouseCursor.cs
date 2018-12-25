using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public Vector3 mousePosition;

	// Update is called once per frame
	void Update () {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // plane.Raycast returns the distance from the ray start to the hit point
        var plane = new Plane(Vector3.back, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            transform.position = hitPoint;
            mousePosition = hitPoint;
        }
    }

    public Vector3 VecToMouse(GameObject other)
    {
        Vector3 toMouse =(mousePosition - other.transform.position);
        toMouse.z = 0;
        return toMouse.normalized;
    }
}
