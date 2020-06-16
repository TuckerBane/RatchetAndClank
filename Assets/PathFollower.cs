using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{

    public PathManager pathToFollow;
    public int pathStartIndex = 0;

    private float speed = 3.0f;
    private int currentPathIndex;
    private float closeEnough = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        currentPathIndex = pathStartIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (VectorHelpers.DistanceBetween(transform.position, pathToFollow.GetNextPoint(currentPathIndex)) < closeEnough)
            ++currentPathIndex;
        transform.position += (pathToFollow.GetNextPoint(currentPathIndex) - transform.position).normalized * speed * Time.deltaTime;
    }
}
