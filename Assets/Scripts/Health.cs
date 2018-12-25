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

    public Timer invincibilityCooldown;
    public int health = 3;



    public bool TakeDamage(DamageInfo damageInfo)
    {
        if (damageInfo.damageDealer == gameObject || damageInfo.damageDealerParent == gameObject)
            return false;
        if (!invincibilityCooldown.resetIfDone())
            return false;

        health -= damageInfo.damage;
        if (health <= 0)
            gameObject.BroadcastMessage("Die", damageInfo);
        return true;
    }
}
