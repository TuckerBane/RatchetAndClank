using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyVision))]
public class ShootAtPlayer : MonoBehaviour {

    public float shotInterval = 3.0f;
    public GameObject bullet;
    public GameObject target;
    Timer shotCooldown;

	// Use this for initialization
	void Start () {
        shotCooldown = new Timer(shotInterval);
        target = GetComponent<EnemyVision>().target;
    }
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<EnemyVision>().canSeeTarget && shotCooldown.resetIfDone())
        {
            Vector3 toTarget = target.transform.position - transform.position;
            toTarget.z = 0;
            toTarget.Normalize();
            GameObject bull = Instantiate(bullet, transform.position, transform.rotation);
            bull.GetComponent<BulletLogic>().Initialize(transform.position, toTarget, gameObject);
            foreach (BulletComponentBase comp in bull.GetComponentsInChildren<BulletComponentBase>())
                comp.InitializeWeaponComponents();
        }
	}
}
