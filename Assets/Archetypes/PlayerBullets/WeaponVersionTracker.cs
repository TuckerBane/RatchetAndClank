using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponModVersions
{
    public GameObject bulletBase;
    public GameObject[] array;
}

enum BulletIndex
{
    RapidFire = 0,
    Boomarang = 1,
    Splinter = 2,
    Heavy = 3,
    Omni = 4
}

enum PowerLevel
{
    Normal = 0,
    Lesser = 1
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponVersionTracker", order = 1)]
public class WeaponVersionTracker : ScriptableObject {
    public WeaponModVersions[] weaponVersions;
}
