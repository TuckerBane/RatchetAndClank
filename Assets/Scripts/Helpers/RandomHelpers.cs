using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct ValueRange
{
    public float min, max;
    public float value { get { return Random.Range(min, max); } }
}

