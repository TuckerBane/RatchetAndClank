using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate Vector3 VelocityCalculator(Vector3 velocitySoFar);

public class MovementEffect
{
    public Vector3 velocity;
    public Timer duration;
    public AnimationCurve powerCurve;
    public VelocityCalculator velocityCalculator;
    public string id;

    public MovementEffect(Vector3 velocity, float duration = float.MaxValue, AnimationCurve powerCurve = null, VelocityCalculator velocityCalculator = null, string id = null)
    {
        this.velocity = velocity;
        this.duration = new Timer(duration);
        this.powerCurve = powerCurve;
        this.velocityCalculator = velocityCalculator;
        this.id = id;
    }

    public MovementEffect(VelocityCalculator velocityCalculator, string id, float duration = float.MaxValue)
    {
        this.velocity = Vector3.zero;
        this.duration = new Timer(duration);
        this.powerCurve = null;
        this.velocityCalculator = velocityCalculator;
        this.id = id;
    }

    public bool IsDone()
    {
        return duration.isDone();
    }

    public Vector3 GetNewVelocity()
    {
        Vector3 newVel = velocity;
        if (powerCurve != null)
            newVel *= duration.interpolationValue();
        if (velocityCalculator != null)
            newVel = velocityCalculator(newVel);
        return newVel;
    }

}

[RequireComponent(requiredComponent: typeof(Rigidbody))]
public class FakeRigidBody : MonoBehaviour {
    public float speedMod = 1.0f;
    public List<MovementEffect> movementEffects = new List<MovementEffect>();

    public void AddEffect(MovementEffect effect)
    {
        movementEffects.Add(effect);
    }

    public void RemoveEffect(string id)
    {
        movementEffects.Remove(movementEffects.Find(effect => {return effect.id == id; } ) );
    }
    public void RemoveAllEffects()
    {
        movementEffects.Clear();
    }

    public bool HasEffect(string id)
    {
        return GetEffect(id) != null;
    }

    public MovementEffect GetEffect(string id)
    {
        return movementEffects.Find(effect => { return effect.id == id; });
    }

    private void FixedUpdate()
    {
        HashSet<MovementEffect> effectsToRemove = new HashSet<MovementEffect>();
        var obj =gameObject;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        foreach (MovementEffect effect in movementEffects)
        {
            if(effect.IsDone())
            {
                effectsToRemove.Add(effect);
                continue;
            }
            GetComponent<Rigidbody>().velocity += (effect.GetNewVelocity() * speedMod);
        }
        movementEffects = movementEffects.FindAll(effect => { return !effectsToRemove.Contains(effect); });
    }

}
