using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePitLogic : MonoBehaviour {

    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        other.BroadcastMessage("TakeDamage", new DamageInfo(damage, gameObject, gameObject), SendMessageOptions.DontRequireReceiver);
    }

    private void OnTriggerStay(Collider other)
    {
        other.BroadcastMessage("TakeDamage", new DamageInfo(damage, gameObject, gameObject), SendMessageOptions.DontRequireReceiver);
    }
}
