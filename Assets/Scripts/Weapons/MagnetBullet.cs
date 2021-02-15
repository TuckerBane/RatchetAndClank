using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBullet : BulletComponentBase
{

    static Dictionary< GameObject, GameObject > lastHitTracker = new Dictionary<GameObject, GameObject>();

    public float duration = 10.0f;
    public float pullSpeed = 300.0f;
    public GameObject visualEffectArchetype;
    public int collisionDamage = 1;

    public override void BulletDie(Collision killedBy)
    {
        GameObject shooter = GetComponentInParent<BulletLogic>().creator;
        if(!lastHitTracker.ContainsKey(shooter))
            lastHitTracker.Add(shooter, null);
        lastHitTracker.TryGetValue(shooter, out GameObject lastHit);

        if (killedBy.gameObject.TryGetComponent(out MagnetEffectTracker comp))
        {
            // if we've hit the same target again, refresh the timer
            if (killedBy.gameObject == lastHit)
            {
                comp.startTime = Time.time;
                comp.duration = duration;
            }
            return;
        }

        // add the visual effect
        GameObject visualEffect = VisualsHelpers.AddFloatingIconToObject(visualEffectArchetype, killedBy.gameObject);

        // add the tracking component
        MagnetEffectTracker newTracker = killedBy.gameObject.AddComponent<MagnetEffectTracker>();
        newTracker.speed = pullSpeed;
        newTracker.startTime = Time.time;
        newTracker.duration = duration;
        newTracker.visualEffect = visualEffect;
        newTracker.damange = collisionDamage;

        // add the velocity effect
        if (killedBy.gameObject.TryGetComponent(out FakeRigidBody body))
        {
            VelocityCalculator calculator = newTracker.MagnetEffect;
            if (body.HasEffect("MagnetEffect"))
                body.RemoveEffect("MagnetEffect"); // should never happen, in theory
            killedBy.gameObject.GetComponent<FakeRigidBody>().AddEffect(new MovementEffect(calculator, "MagnetEffect", duration));
        }



        // if we have a valid first half of a connection 
        if (lastHit && lastHit.TryGetComponent(out MagnetEffectTracker oldTracker))
        {
            lastHitTracker[shooter] = null;

            // start the effect
            newTracker.otherEnd = oldTracker.gameObject;
            oldTracker.otherEnd = newTracker.gameObject;
            newTracker.startTime = Time.time;
            newTracker.duration = duration;
            oldTracker.startTime = Time.time;
            oldTracker.duration = duration;
        }
        else
        {
            lastHitTracker[shooter] = newTracker.gameObject;
        }

    }
}

public class MagnetEffectTracker : MonoBehaviour
{
    public GameObject otherEnd;
    public GameObject visualEffect;
    public float speed = 5.0f;
    public float startTime = 0.0f;
    public float duration = 0.0f;
    public float minTravelTime = 0.5f;
    public bool hasMadeContact = false;
    public int damange;

    public Vector3 MagnetEffect(Vector3 prev)
    {
        if (!otherEnd)
            return Vector3.zero;

        Vector3 toTarget = (-gameObject.transform.position + otherEnd.transform.position).normalized;

        return toTarget * speed * Time.deltaTime;
    }

    public void Update()
    {
        if (Time.time > startTime + duration)
        {
            Destroy(visualEffect);
            Destroy(this);
        }
    }

    public void OnDestroy()
    {
        if (visualEffect)
            Destroy(visualEffect);

        if(otherEnd)
        {
            Destroy(otherEnd.GetComponent<MagnetEffectTracker>());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!hasMadeContact && other.gameObject == otherEnd && other.gameObject.TryGetComponent(out Health health))
        {
            if(Time.time - startTime > minTravelTime)
                health.TakeDamage(new DamageInfo(damange, gameObject, gameObject));
            hasMadeContact = true;
        }
    }

}
