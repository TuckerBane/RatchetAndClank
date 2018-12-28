using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterBallLogic : MonoBehaviour {

    public int damageValue = 15;
    public int splitsRemaining = 3;

    private void HandleCollision(Collision collision)
    {
        if (collision.gameObject == GetComponent<EnemyVision>().target)
        {
            collision.gameObject.BroadcastMessage("TakeDamage", new DamageInfo(damageValue, gameObject, gameObject), SendMessageOptions.DontRequireReceiver);
            gameObject.BroadcastMessage("Die", new DamageInfo(0, collision.gameObject, collision.gameObject));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        HandleCollision(collision);
    }


    public void Die(DamageInfo info)
    {
        if (info.damageDealer != null)
        {
            Vector3 toKiller = (info.damageDealer.transform.position - transform.position).normalized;
            if (splitsRemaining > 0)
            {
                --splitsRemaining;
                transform.localScale /= 2.0f;
                Instantiate(gameObject, transform.position + toKiller.Rotate2D(90) * transform.lossyScale.x, transform.rotation);
                Instantiate(gameObject, transform.position + toKiller.Rotate2D(-90) * transform.lossyScale.x, transform.rotation);
            }
        }
        Destroy(gameObject);
    }
}
