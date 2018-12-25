using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemyMovement : MonoBehaviour {

    public float speed = 5.0f;

    public Vector3 ToTargetMover(Vector3 prev)
    {
        if (!GetComponent<EnemyVision>().canSeeTarget)
            return Vector3.zero;
        Vector3 toPlayer = FindObjectOfType<PlayerMovement>().transform.position - transform.position;
        toPlayer.Normalize();

        GetComponent<RotationHandler>().AddOrUpdateRotation("SimpleEnemyMovement", toPlayer.AsRotation2d());
        return toPlayer * speed;
    }

    // Use this for initialization
    void Awake () {
        GetComponent<FakeRigidBody>().AddEffect(new MovementEffect(Vector3.zero, float.MaxValue, null, ToTargetMover, "ChargeEnemyMovement"));
	}
	
}
