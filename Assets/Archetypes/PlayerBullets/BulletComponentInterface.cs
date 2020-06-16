using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponentBase : MonoBehaviour
{
    public virtual void BulletDie(Collision killedBy) { }

    public virtual void InitializeWeaponComponents() { }

    public virtual void InitializeWeapon(GameObject player) { }
}
