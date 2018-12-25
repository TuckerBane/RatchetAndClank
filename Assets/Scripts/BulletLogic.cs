using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletLogic : MonoBehaviour {

    public float spawnDistance = 2.0f;
    public float bulletSpeed = 10.0f;
    public int damage = 1;
    public GameObject creator { get; private set; }

    public void Initialize(Vector3 position, Vector3 toMouse, GameObject creator)
    {
        this.creator = creator;
        transform.position = position + toMouse * spawnDistance;
        GetComponent<FakeRigidBody>().AddEffect(new MovementEffect(toMouse * bulletSpeed, float.MaxValue,null,null,"BulletLogic"));
        GetComponent<Rigidbody>().velocity = toMouse * bulletSpeed;
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != gameObject)
        {
            other.gameObject.SendMessage("TakeDamage", new DamageInfo(damage, gameObject, creator), SendMessageOptions.DontRequireReceiver);
            gameObject.BroadcastMessage("BulletDie", other);
        }
    }

}
