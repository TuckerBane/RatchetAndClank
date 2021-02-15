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

    private static int PlayerDashingLayerEnum = 15;
    private static int PlayerLayerEnum = 11;

	// Use this for initialization
	void Start () {
        dashCooldownTimer = new Timer(dashCooldownTime);
        dashActiveTimer = new Timer(dashActiveTime);
        VelocityCalculator calculator = NormalMovementCalculator;
        GetComponent<FakeRigidBody>().AddEffect(new MovementEffect(calculator,"playerMovement"));
    }
	
    Vector3 NormalMovementCalculator(Vector3 oldVel)
    {
        if (dashActiveTimer.running())
        {
            return dashDirection * dashSpeed;
        }
        else if (gameObject.layer == PlayerDashingLayerEnum)
            gameObject.layer = PlayerLayerEnum;


        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
            movement += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            movement += Vector3.right;
        if (Input.GetKey(KeyCode.S))
            movement += Vector3.down;
        if (Input.GetKey(KeyCode.W))
            movement += Vector3.up;

        if (movement != Vector3.zero)
        {
            movement.Normalize();
            if (Input.GetKey(KeyCode.Space) && dashCooldownTimer.isDone())
            {
                gameObject.layer = PlayerDashingLayerEnum;
                dashDirection = movement;
                dashCooldownTimer.reset();
                dashActiveTimer.reset();
                return dashDirection * dashSpeed;
            }

        }
        return oldVel + movement * speed;
    }

	// Update is called once per frame
	void Update () {
        /*
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
        */
    }

}
