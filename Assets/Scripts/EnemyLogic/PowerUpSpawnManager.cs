using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    public int curDeathCount = 0;
    public int curDeathThreshhold = 0;
    public int minDeathThreshhold = 4;
    public int maxDeathThreadhold = 6;

    public int curHealthDeathCount = 0;
    public int curHealthDeathThreshhold = 0;
    public int minDeathHealthThreshhold = 15;
    public int maxDeathHealthThreadhold = 20;
    public GameObject[] possiblePowerups;
    public GameObject healthPowerup;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePowerupDeathThreshhold();
        GenerateHealthDeathThreshhold();
    }

    private void GeneratePowerupDeathThreshhold()
    {
        curDeathThreshhold = Random.Range(minDeathThreshhold, maxDeathThreadhold + 1);
       
    }

    private void GenerateHealthDeathThreshhold()
    {
        curHealthDeathThreshhold = Random.Range(minDeathHealthThreshhold, maxDeathHealthThreadhold + 1);
    }

    // null if you shouldn't spawn a Powerup
    public GameObject GetPowerupArchetypeToSpawnOnDeath()
    {
        ++curDeathCount;
        ++curHealthDeathCount;
        if (curDeathCount >= curDeathThreshhold)
        {
            curDeathCount = 0;
            GeneratePowerupDeathThreshhold();
            return possiblePowerups[Random.Range(0, possiblePowerups.Length - 1)];
        }
        else if (curHealthDeathCount >= curHealthDeathThreshhold)
        {
            curHealthDeathCount = 0;
            GenerateHealthDeathThreshhold();
            return healthPowerup;
        }
        return null;
    }

}
