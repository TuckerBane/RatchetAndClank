using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarLogic : MonoBehaviour {

    private GameObject forground, background;
    private GameObject indirectParent;

	// Use this for initialization
	void Start () {
        forground = GetComponentInChildren<IsForground>().gameObject;
        background = GetComponentInChildren<IsBackground>().gameObject;
        indirectParent = GetComponent<PositionOnlyParent>()?.parent;
    }
	
	// Update is called once per frame
	void Update () {
        if (!indirectParent && transform.parent)
        {
            float curHealth = GetComponentInParent<Health>().health;
            float startingHealth = GetComponentInParent<Health>().startingHealth;
            float curHealthRatio = curHealth / startingHealth;
            forground.transform.localPosition = background.transform.localPosition + (Vector3.back * 0.001f) + Vector3.left * (background.transform.lossyScale.x * (1.0f - curHealthRatio) / 2);
            forground.transform.localScale = background.transform.localScale - Vector3.right * (background.transform.localScale.x * (1.0f - curHealthRatio) - 0.05f);
        }
        else if(indirectParent)
        {
            float curHealth = indirectParent.GetComponentInParent<Health>().health;
            float startingHealth = indirectParent.GetComponentInParent<Health>().startingHealth;
            float curHealthRatio = curHealth / startingHealth;
            forground.transform.localPosition = background.transform.localPosition + (Vector3.back * 0.001f) + Vector3.left * (background.transform.lossyScale.x * (1.0f - curHealthRatio) / 2);
            forground.transform.localScale = background.transform.localScale - Vector3.right * (background.transform.localScale.x * (1.0f - curHealthRatio) - 0.05f);
        }
 
    }
}
