using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : MonoBehaviour
{
    public float fireRateMod = 0.3f;
    public void InitializeWeaponComponents()
    {
        FindObjectOfType<PlayerShooting>().currentEffects.fireRateMod = fireRateMod;
    }
}
