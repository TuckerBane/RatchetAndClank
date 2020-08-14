using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaybePowerUpOnDeath : MonoBehaviour
{
    public void Die(DamageInfo info)
    {
        GameObject powerupArchetype = FindObjectOfType<PowerUpSpawnManager>().GetPowerupArchetypeToSpawnOnDeath();
        if(powerupArchetype)
        {
            Instantiate(powerupArchetype, transform.position, powerupArchetype.transform.rotation);
        }
    }
}
