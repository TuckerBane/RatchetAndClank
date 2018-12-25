using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakpoint : MonoBehaviour {

    int damageMultipler = 1000;

    public bool TakeDamage(DamageInfo damageInfo)
    {
        damageInfo.damage *= damageMultipler;
        transform.parent.SendMessage("TakeDamage", damageInfo);
        return true;
    }
}
