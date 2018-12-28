using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour {

    public float speed = 2.0f;
    public float directionSwitchRate = 3.0f;
    private Timer directionSwitchTimer;
    private int directionEnum = 0;

    public Vector3 ToTargetMover(Vector3 prev)
    {
        if(directionSwitchTimer.isDone())
        {
            directionSwitchTimer = new Timer(directionSwitchRate);
            directionEnum += Random.Range(1, 2);
        }


        if (!GetComponent<EnemyVision>().canSeeTarget)
            return Vector3.zero;
        Vector3 toPlayer = FindObjectOfType<PlayerMovement>().transform.position - transform.position;
        toPlayer.Normalize();
        switch(directionEnum % 3)
        {
            case 0:
                break;
            case 1:
                toPlayer = toPlayer.Rotate2D(90);
                break;
            case 2:
                toPlayer = toPlayer.Rotate2D(-90);
                break;
        }
        GetComponent<RotationHandler>().AddOrUpdateRotation("SimpleEnemyMovement",toPlayer.AsRotation2d());
        return toPlayer * speed;
    }

    // Use this for initialization
    void Start () {
        directionSwitchTimer = new Timer(directionSwitchRate);
        MovementEffect movement = new MovementEffect(Vector3.zero, float.MaxValue, null, ToTargetMover, "SimpleEnemyMovement");
        GetComponent<FakeRigidBody>().AddEffect(movement);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
