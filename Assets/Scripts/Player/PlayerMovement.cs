using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(requiredComponent: typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    public float speed = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
            movement += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            movement += Vector3.right;
        if (Input.GetKey(KeyCode.S))
            movement += Vector3.down;
        if (Input.GetKey(KeyCode.W))
            movement += Vector3.up;

        if(movement != Vector3.zero)
        {
            movement.Normalize();
        }
        GetComponent<Rigidbody>().velocity = movement * speed;

    }
}
