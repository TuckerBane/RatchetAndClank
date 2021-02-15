using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomarang : BulletComponentBase {

    private GameObject bull;
    private GameObject player;

    public int remainingBounces = 1;

    private bool returning = false;
    private Vector3 outgoingVelocity;
    public float speed = 15.0f;
    public Timer speedTimer;
    public Timer autoReturnTimer;
    public AnimationCurve speedCurve;
    public float returnBounceDistance = 0.75f;

    private void Awake()
    {

    }

    public override void InitializeWeaponComponents()
    {
        speedTimer.reset();
        autoReturnTimer.reset();
        bull = transform.parent.gameObject;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        Destroy(GetComponentInParent<DieInstantly>());
        GetComponentInParent<FakeRigidBody>().AddEffect(new MovementEffect(Vector3.zero, float.MaxValue, null, ReturnVelocityGetter, "Returning"));
        outgoingVelocity = GetComponentInParent<FakeRigidBody>().GetEffect("BulletLogic").velocity.normalized;
        GetComponentInParent<FakeRigidBody>().RemoveEffect("BulletLogic");
    }

    public Vector3 ReturnVelocityGetter(Vector3 prev)
    {
        Vector3 toPlayer = player.transform.position - transform.parent.position;
        toPlayer.Normalize();
        if (!returning)
            toPlayer = outgoingVelocity;
        return toPlayer * speed * speedCurve.Evaluate(speedTimer.interpolationValue());
    }

    private void Update()
    {
        if(!returning && autoReturnTimer.resetIfDone())
        {
            if (remainingBounces > 0)
            {
                TurnAround();
            }
        }
        else if (returning)
        {
            Vector3 toPlayer = player.transform.position - transform.parent.position;
            toPlayer.z = 0;
            if (toPlayer.magnitude <= returnBounceDistance)
            {
                TurnAround();
            }
        }
    }

    private void TurnAround()
    {
        autoReturnTimer.reset();
        speedTimer = new Timer(0.5f);
        --remainingBounces;
        returning = !returning;
    }

    public override void BulletDie(Collision killedBy)
    {
        if (killedBy.gameObject == player && !returning)
            return;

        if(remainingBounces > 0)
        {
            TurnAround();
        }
        else
        {
            Destroy(bull);
        }
    }
}
