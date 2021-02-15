using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBullets : BulletComponentBase
{
    public float duration = 2.0f;
    public float speedMod = 0.5f;
    public AnimationCurve pushSpeedCurve = AnimationCurve.Constant(0, 1, 1);

    public int bonusDamage = 5;

    private Vector3 oldVelocity;

    public override void BulletDie(Collision killedBy)
    {
        if(killedBy.gameObject.GetComponent<FakeRigidBody>())
        {
            killedBy.gameObject.GetComponent<FakeRigidBody>().AddEffect(new MovementEffect(oldVelocity * speedMod, duration, pushSpeedCurve, null, "HeavyBullets"));
        }
    }

    public override void InitializeWeaponComponents()
    {
        GetComponentInParent<BulletLogic>().damage += bonusDamage;
    }

        public void FixedUpdate()
    {
        oldVelocity = GetComponentInParent<Rigidbody>().velocity;
    }

}
