using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IsPlayer playerShooting))
        {
            if (other.GetComponent<Health>().Heal(healthAmount))
                Destroy(gameObject);
        }
    }
}
