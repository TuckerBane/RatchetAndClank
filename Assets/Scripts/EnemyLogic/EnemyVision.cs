using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {

    public GameObject target;
    public bool canSeeTarget; 

	// Use this for initialization
	void Awake () {
        // TODO: allow multiple players?
        target = FindObjectOfType<PlayerShooting>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        canSeeTarget = false;
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = new Ray(transform.position, target.transform.position - transform.position);
        LayerMask mask = (LayerMask.GetMask("Walls") | LayerMask.GetMask("Players"));
        if (Physics.Raycast(ray,out hitInfo, Mathf.Infinity, mask))
        {
            if (hitInfo.transform.gameObject == target)
                canSeeTarget = true;
        }
        
    }

    public Vector3 ToTarget()
    {
        return (target.transform.position - transform.position).normalized;
    }
}
