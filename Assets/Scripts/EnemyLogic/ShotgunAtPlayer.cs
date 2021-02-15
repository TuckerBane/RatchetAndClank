using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyVision))]
public class ShotgunAtPlayer : MonoBehaviour
{

    public float shotInterval = 3.0f;
    public float spreadAngleDegrees = 90.0f;
    public int numBullets = 9;
    public GameObject bullet;
    public GameObject target;
    Timer shotCooldown;

    // Use this for initialization
    void Start()
    {
        shotCooldown = new Timer(shotInterval);
        target = GetComponent<EnemyVision>().target;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toTarget = target.transform.position - transform.position;
        toTarget.z = 0;
        toTarget.Normalize();

        TryGetComponent(out RotationHandler rotationHandler);
        if (rotationHandler && Mathf.Abs( (toTarget.AsAngle2D() - rotationHandler.GetCurrentAngle2D()) % 360) > 45.0f)
            return;

        if (GetComponent<EnemyVision>().canSeeTarget && shotCooldown.resetIfDone())
        {
            for(int i = 0; i < numBullets; ++i)
            {
                float fireAngle = spreadAngleDegrees / ((float)numBullets - 1)  * (float)i - spreadAngleDegrees / 2.0f;

                GameObject bull = Instantiate(bullet, transform.position, transform.rotation);
                bull.GetComponent<BulletLogic>().Initialize(transform.position, toTarget.Rotate2D(fireAngle), gameObject);
                foreach (BulletComponentBase comp in bull.GetComponentsInChildren<BulletComponentBase>())
                    comp.InitializeWeaponComponents();
            }
        }
    }
}

