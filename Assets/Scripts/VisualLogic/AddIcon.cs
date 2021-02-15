using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddIcon : MonoBehaviour
{
    public GameObject IconArchetype;

    // Start is called before the first frame update
    void Start()
    {
        VisualsHelpers.AddFloatingIconToObject(IconArchetype, gameObject);
    }
}
