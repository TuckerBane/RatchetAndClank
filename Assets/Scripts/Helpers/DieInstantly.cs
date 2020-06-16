using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieInstantly : MonoBehaviour {

    public void Die(DamageInfo info)
    {
        Destroy(gameObject);
    }

    public void BulletDie(Collision info)
    {
        Destroy(gameObject);
    }
}
