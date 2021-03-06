﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{

    public Quaternion baseValue;
    public Dictionary<string, Quaternion> rotations;


    // Use this for initialization
    void Awake()
    {
        rotations = new Dictionary<string, Quaternion>();
        baseValue = transform.rotation;
    }

    public void AddOrUpdateRotation(string name, Quaternion value)
    {
        rotations[name] = value;
        transform.rotation = Quaternion.identity;
        foreach(Quaternion rot in rotations.Values)
            transform.rotation *= rot;
        transform.rotation *= baseValue;
    }

    public Quaternion GetCurrentRotation() 
    {
        Quaternion result = Quaternion.identity;
        foreach (Quaternion rot in rotations.Values)
            result *= rot;
        return result;
    }

    public float GetCurrentAngle2D()
    {
        Quaternion rotation = GetCurrentRotation();
        return rotation.eulerAngles.z;
    }

}
