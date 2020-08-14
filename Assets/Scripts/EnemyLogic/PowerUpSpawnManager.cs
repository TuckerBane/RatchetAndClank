using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    public int curDeathCount = 0;
    public int curDeathThreshhold = 0;
    public int minDeathThreshhold = 4;
    public int maxDeathThreadhold = 6;
    public GameObject[] possiblePowerups;

    // Start is called before the first frame update
    void Start()
    {
        GenerateDeathThreshhold();
    }

    private void GenerateDeathThreshhold()
    {
        curDeathThreshhold = Random.Range(minDeathThreshhold, maxDeathThreadhold + 1);
    }

    // null if you shouldn't spawn a Powerup
    public GameObject GetPowerupArchetypeToSpawnOnDeath()
    {
        ++curDeathCount;
        if(curDeathCount >= curDeathThreshhold)
        {
            curDeathCount = 0;
            GenerateDeathThreshhold();
            return possiblePowerups[Random.Range(0, possiblePowerups.Length - 1)];
        }
        return null;
    }

}
