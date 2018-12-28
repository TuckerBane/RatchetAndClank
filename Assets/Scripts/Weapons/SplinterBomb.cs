using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplinterBomb : MonoBehaviour {

    public float minAngleDeg = 10.0f;
    public float maxAngleDeg = 30.0f;
    public int splinters = 3;

    public void BulletDie(Collision killedBy)
    {
        if(killedBy == null)
        {
            return;
        }

        var shooting = FindObjectOfType<PlayerShooting>();
        Vector3 collisionNormal = killedBy.contacts[0].normal;
        collisionNormal.z = 0;
        collisionNormal.Normalize();

        for(int i = 0; i < splinters; ++i)
        {
            Vector3 fireDirection = collisionNormal.Rotate2D(Random.Range(minAngleDeg,maxAngleDeg) * (Random.value > 0.5f ? 1 : -1));
            GameObject bull = shooting.GetUninitializedBullet();
            bull.GetComponent<Transform>().localScale *= 0.5f;
            Destroy(bull.GetComponentInChildren<SplinterBomb>());
            bull.GetComponent<BulletLogic>().Initialize(transform.parent.position, fireDirection, GetComponentInParent<BulletLogic>().creator);
            bull.BroadcastMessage("InitializeWeaponComponents", SendMessageOptions.DontRequireReceiver);
            bull.GetComponent<BulletLogic>().damage = Mathf.Min(1, bull.GetComponent<BulletLogic>().damage / 2);
        }
        Destroy(this);
    }
}
