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
    public static float AsAngle2D(this Vector3 vec)
    {
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        return angle;
    }


    public static float DistanceBetween(Vector3 t1, Vector3 t2)
    {
        return (t1 - t2).magnitude;
    }

    public static float DistanceBetween(Transform t1, Transform t2)
    {
        return DistanceBetween(t1.position, t2.position);
    }


    public static Vector3 Times(this Vector3 vec, float f)
    {
        return new Vector3(vec.x * f, vec.y * f, vec.z * f);
    }

}
