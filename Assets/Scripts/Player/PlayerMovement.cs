using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(requiredComponent: typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    public float speed = 2.0f;
    public float dashCooldownTime = 1.0f;
    public float dashActiveTime = 0.2f;
    private Timer dashCooldownTimer;
    private Timer dashActiveTimer;
    public float dashSpeed = 20.0f;
    private Vector3 dashDirection;

	// Use this for initialization
	void Start () {
        dashCooldownTimer = new Timer(dashCooldownTime);
        dashActiveTimer = new Timer(dashActiveTime);
    }
	
	// Update is called once per frame
	void Update () {
        if (dashActiveTimer.running())
        {
            GetComponent<Rigidbody>().velocity = dashDirection * dashSpeed;
            return;
        }
            

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
            if (Input.GetKey(KeyCode.Space) && dashCooldownTimer.isDone())
            {
                dashDirection = movement;
                dashCooldownTimer.reset();
                dashActiveTimer.reset();
                GetComponent<Rigidbody>().velocity = dashDirection * dashSpeed;
                return;
            }

        }
        GetComponent<Rigidbody>().velocity = movement * speed;

    }
}
