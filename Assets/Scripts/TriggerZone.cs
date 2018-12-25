using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TriggerZone : MonoBehaviour
{

    public GameObject[] activators;
    public GameObject[] activated;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject activator in activators)
        {
            if (activator == other.gameObject)
            {
                foreach(GameObject obj in activated)
                {
                    if (obj)
                        obj.SetActive(true);
                }
                break;
            }
        }
    }
}
