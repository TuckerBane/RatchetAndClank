using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Returning : MonoBehaviour {

    private GameObject bull;

    public int remainingBounces = 1;

    private bool returning = false;
    public float speed = 15.0f;
    public Timer returnProgress;
    public AnimationCurve returnSpeed;

    private void Awake()
    {
        bull = transform.parent.gameObject;
    }

    public void InitializeWeaponComponents()
    {
        Destroy(GetComponentInParent<DieInstantly>());
        GetComponentInParent<FakeRigidBody>().AddEffect(new MovementEffect(Vector3.zero, float.MaxValue, null, ReturnVelocityGetter, "Returning"));
        GetComponentInParent<FakeRigidBody>().RemoveEffect("BulletLogic");
    }

    public Vector3 ReturnVelocityGetter(Vector3 prev)
    {
        Vector3 toPlayer = FindObjectOfType<PlayerMovement>().transform.position - transform.parent.position;
        toPlayer.Normalize();
        if (!returning)
            toPlayer = -toPlayer;
        return toPlayer * speed * returnSpeed.Evaluate(returnProgress.interpolationValue());
    }

    public void BulletDie(Collision killedBy)
    {
        if(remainingBounces > 0)
        {
            returnProgress = new Timer(0.5f);
            --remainingBounces;
            returning = !returning;
        }
        else
        {
            Destroy(bull);
        }
    }
}
