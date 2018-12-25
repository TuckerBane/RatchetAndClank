using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorHelpers {
    public static Vector3 Rotate2D(this Vector3 vec, float angleDeg)
    {
        angleDeg *= Mathf.Deg2Rad;
        return new Vector3(vec.x * Mathf.Cos(angleDeg) - vec.y * Mathf.Sin(angleDeg), vec.x * Mathf.Sin(angleDeg) + vec.y * Mathf.Cos(angleDeg), 0);
    }
    public static Quaternion AsRotation2d(this Vector3 vec)
    {
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        return q;
    }
}
