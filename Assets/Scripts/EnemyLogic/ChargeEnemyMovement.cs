using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemyMovement : MonoBehaviour {

    public float speed = 5.0f;
    public float maxRotationPerSecondDegrees = 180.0f;

    public Vector3 ToTargetMover(Vector3 prev)
    {
        if (!GetComponent<EnemyVision>().canSeeTarget)
            return Vector3.zero;
        Vector3 toPlayer = FindObjectOfType<PlayerMovement>().transform.position - transform.position;
        toPlayer.Normalize();
        Quaternion newRotation;
        if(GetComponent<RotationHandler>().rotations.TryGetValue("SimpleEnemyMovement", out Quaternion oldRotation))
        {
            Quaternion goalRotation = toPlayer.AsRotation2d();
            newRotation = Quaternion.RotateTowards(oldRotation, goalRotation, maxRotationPerSecondDegrees * Time.deltaTime);
        }
        else
        {
            newRotation = toPlayer.AsRotation2d();
        }

        GetComponent<RotationHandler>().AddOrUpdateRotation("SimpleEnemyMovement", newRotation);
        return (newRotation * Vector3.right).normalized * speed;
    }

    // Use this for initialization
    void Awake () {
        GetComponent<FakeRigidBody>().AddEffect(new MovementEffect(Vector3.zero, float.MaxValue, null, ToTargetMover, "ChargeEnemyMovement"));
	}
	
}
