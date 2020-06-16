using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : BulletComponentBase
{
    public float fireRateMod = 0.3f;
    public override void InitializeWeapon(GameObject player)
    {
       player.GetComponent<PlayerShooting>().currentEffects.fireRateMod = fireRateMod;
    }
}
