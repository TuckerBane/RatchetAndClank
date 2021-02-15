using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDeathTracker
{
    public int deaths = 0;
    public int maxDeaths = 0;
}

public class SplitterBallLogic : MonoBehaviour {

    public int damageValue = 15;
    public int splitsRemaining = 3;
    private bool isBaseUnit = true;
    [HideInInspector]
    public BallDeathTracker ballDeathTracker;

    public void Start()
    {
        if (isBaseUnit)
        {
            ballDeathTracker = new BallDeathTracker();
            ballDeathTracker.maxDeaths = (1 << (splitsRemaining + 1)) - 1;
        }
    }

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
                isBaseUnit = false;
                --splitsRemaining;
                transform.localScale /= 2.0f;
                GameObject ball1 = Instantiate(gameObject, transform.position + toKiller.Rotate2D(90) * transform.lossyScale.x, transform.rotation);
                ball1.GetComponent<SplitterBallLogic>().ballDeathTracker = ballDeathTracker;
                ball1.GetComponent<SplitterBallLogic>().isBaseUnit = false;

                GameObject ball2 = Instantiate(gameObject, transform.position + toKiller.Rotate2D(-90) * transform.lossyScale.x, transform.rotation);
                ball2.GetComponent<SplitterBallLogic>().ballDeathTracker = ballDeathTracker;
                ball2.GetComponent<SplitterBallLogic>().isBaseUnit = false;

                ball1.transform.parent = transform.parent;
                ball2.transform.parent = transform.parent;
            }
        }
        else
        {
            Debug.LogWarning("Splitter ball killed by null damange dealer");
        }
        ++ballDeathTracker.deaths;
        if(ballDeathTracker.deaths == ballDeathTracker.maxDeaths)
        {
            var spawnManager = FindObjectOfType<PowerUpSpawnManager>();
            GameObject powerupArchetypeToSpawn = spawnManager.possiblePowerups[Random.Range(0, spawnManager.possiblePowerups.Length - 1)];
            Instantiate(powerupArchetypeToSpawn, transform.position, powerupArchetypeToSpawn.transform.rotation);
        }

        Destroy(gameObject);
    }
}
