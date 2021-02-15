using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DamageInfo
{
    public int damage;
    public GameObject damageDealer;
    public GameObject damageDealerParent;
    public Collision collision;

    public Timer invincibilityTimer;

    public DamageInfo(int damage, GameObject damageDealer, GameObject damageDealerParent)
    {
        this.damage = damage;
        this.damageDealer = damageDealer;
        this.damageDealerParent = damageDealerParent;
    }
}

public class DeathInfo
{
    public Collision collision;
}

public class Health : MonoBehaviour {

    public bool isPlayer = false;
    public bool canBeInvincible = false;
    public bool ignoreDamageValues = false;
    public Timer invincibilityCooldown;
    [HideInInspector]
    public int health = 30;
    public int startingHealth = 30;
    public int pointValue = 0;
    // concider making a system for color effects
    public Color damagedColor = Color.yellow;
    public AnimationCurve damagedColorFlashCurve = AnimationCurve.EaseInOut(0,0,1,0);
    private Color originalColor;

    private void Start()
    {
        health = startingHealth;
        originalColor = GetComponent<MeshRenderer>().material.color;
    }

    private void Update()
    {
        GetComponent<MeshRenderer>().material.color = Color.Lerp(originalColor, damagedColor, damagedColorFlashCurve.Evaluate(invincibilityCooldown.interpolationValue()) );
    }

    public bool TakeDamage(DamageInfo damageInfo)
    {
        if (damageInfo.damageDealer == gameObject || damageInfo.damageDealerParent == gameObject)
            return false;
        if (!invincibilityCooldown.resetIfDone() && canBeInvincible)
            return false;

        if (ignoreDamageValues)
            --health;
        else
            health -= damageInfo.damage;

        if (health <= 0)
        {
            FindObjectOfType<ScoreDisplay>()?.AddPoints(pointValue);
            gameObject.BroadcastMessage("Die", damageInfo, SendMessageOptions.DontRequireReceiver);
        }
        return true;
    }

    public bool Heal(int amount)
    {
        if(health < startingHealth)
        {
            health = Math.Min(health + amount, startingHealth);
            return true;
        }
        return false;
    }
}
