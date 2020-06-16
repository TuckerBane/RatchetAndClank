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

    public bool canBeInvincible = false;
    public Timer invincibilityCooldown;
    public int health = 3;
    public int startingHealth = 30;
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

        health -= damageInfo.damage;
        if (health <= 0)
            gameObject.BroadcastMessage("Die", damageInfo);
        return true;
    }
}
