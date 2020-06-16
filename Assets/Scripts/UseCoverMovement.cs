using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseCoverMovement : MonoBehaviour {

    enum Behavior
    {
        BackingUp = 0,
        Hiding,
        Shooting
    }
    Behavior currentBehavior = Behavior.BackingUp;

    Vector3 ProxyVelocity(Vector3 current)
    {
        return velocityProxy;
    }
    private Vector3 velocityProxy = Vector3.zero;
    private Timer behaviorTimer;
    public float backupSpeed = 3.0f;

    // Use this for initialization
    void Start () {
        GetComponent<FakeRigidBody>().AddEffect(new MovementEffect(Vector3.zero, velocityCalculator: ProxyVelocity));
        ChangeBehavior(currentBehavior);
    }
	
	// Update is called once per frame
	void Update () {
		switch(currentBehavior)
        {
            case Behavior.BackingUp:
                BackingUpUpdate();
                break;

            case Behavior.Hiding:
                break;

            case Behavior.Shooting:
                break;
        }
	}

    void ChangeBehavior(Behavior newBehavior)
    {
        switch (currentBehavior)
        {
            case Behavior.BackingUp:
                behaviorTimer = new Timer(3.0f);
                break;

            case Behavior.Hiding:
                break;

            case Behavior.Shooting:
                break;
        }
        currentBehavior = newBehavior;
    }

    private void BackingUpUpdate()
    {
        velocityProxy = -GetComponent<EnemyVision>().ToTarget() * backupSpeed;
    }
}
