using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : BulletComponentBase
{


    public float duration = 3.0f;
    public float tickDuration = 0.2f;
    public int tickDamage = 1;
    public GameObject visualEffectArchetype;

    public override void BulletDie(Collision killedBy)
    {
        if (killedBy.gameObject.TryGetComponent(out FireEffectTracker comp))
        {
            comp.startTime = Time.time;
            comp.duration = duration;
            return;
        }

        // add the visual effect
        GameObject visualEffect = VisualsHelpers.AddFloatingIconToObject(visualEffectArchetype, killedBy.gameObject);

        // add the tracking component
        FireEffectTracker newTracker = killedBy.gameObject.AddComponent<FireEffectTracker>();
        newTracker.startTime = Time.time;
        newTracker.lastDamageTime = Time.time;
        newTracker.duration = duration;
        newTracker.tickDuration = tickDuration;
        newTracker.tickDamage = tickDamage;
        newTracker.visualEffect = visualEffect;
        newTracker.visualEffectArchetype = visualEffectArchetype;
    }
}

public class FireEffectTracker : MonoBehaviour
{
    public GameObject otherEnd;
    public GameObject visualEffect;
    public GameObject visualEffectArchetype;
    public float startTime = 0.0f;
    public float duration = 0.0f;
    public float tickDuration = 0.0f;
    public int tickDamage = 0;
    public float lastDamageTime = 0;


    public void Update()
    {
        // having fire one-shot players would suck
        if (GetComponent<IsPlayer>())
        {
            if(Time.time - lastDamageTime > 2.0f)
            {
                transform.GetComponentInParent<Health>().TakeDamage(new DamageInfo(tickDamage, null, null));
                Destroy(visualEffect);
                Destroy(this);
            }
            return;
        }

        while(Time.time - lastDamageTime >= tickDuration)
        {
            Health health = GetComponentInParent<Health>();
            if (health)
                health.TakeDamage(new DamageInfo(tickDamage, null, null)); // nulls skip "self harm" avoidance
            lastDamageTime += tickDuration;
        }

        if (Time.time > startTime + duration)
        {
            Destroy(visualEffect);
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Health health))
        {
            if (other.gameObject.TryGetComponent(out FireEffectTracker comp))
            {
                comp.startTime = Time.time;
                comp.duration = duration;
                return;
            }

            // add the visual effect
            GameObject visualEffect = VisualsHelpers.AddFloatingIconToObject(visualEffectArchetype, other.gameObject);

            // add the tracking component
            FireEffectTracker newTracker = other.gameObject.AddComponent<FireEffectTracker>();
            newTracker.startTime = Time.time;
            newTracker.lastDamageTime = Time.time;
            newTracker.duration = duration;
            newTracker.tickDuration = tickDuration;
            newTracker.tickDamage = tickDamage;
            newTracker.visualEffect = visualEffect;
            newTracker.visualEffectArchetype = visualEffectArchetype;
        }
    }

}
