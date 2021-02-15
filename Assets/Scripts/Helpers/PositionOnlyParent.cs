using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOnlyParent : MonoBehaviour
{
    public Vector3 localPosition;
    public GameObject parent;

    private void Start()
    {
        // position only parent can't be your real parent
        if (parent && transform.parent == parent.transform)
        {
            Quaternion oldRotation = transform.localRotation;
            transform.parent = null;
            transform.rotation = oldRotation;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.parent && transform.parent == parent.transform)
            transform.parent = null;

        if (!parent)
            Destroy(gameObject);
        else
            transform.position = parent.transform.position + localPosition;
    }
}
