using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private List<Transform> pointsOnPath;

    // Start is called before the first frame update
    void Start()
    {
        pointsOnPath = new List<Transform>();
        foreach (Transform t in transform)
        {
            pointsOnPath.Add(t);
        }
    }

    public Vector3 GetNextPoint(int currentPointIndex)
    {
        return pointsOnPath[(currentPointIndex + 1) % pointsOnPath.Count].position;
    }
}
