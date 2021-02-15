using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject root;
        public float duration = 30.0f;
        public bool waitOnKill = true;
        public int maxChildrenForWaveContinue = 0;
        public bool waitOnTotalClear = true;
    };

    public Wave[] waves;
    public int waveIndex = 0;
    public bool loopWaves = false;
    private GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<SpawnTreeTopNode>().gameObject;
        StartCoroutine(SpawnRoutine());
    }


    IEnumerator SpawnRoutine()
    {
        do
        {
            while (waveIndex < waves.Length)
            {
                Wave wave = waves[waveIndex];

                if(wave.root != null)
                    wave.root.SetActive(true);
                yield return new WaitForSeconds(waves[waveIndex].duration);
                if (wave.waitOnKill)
                {
                    while (wave.root.transform.childCount > wave.maxChildrenForWaveContinue)
                        yield return new WaitForSeconds(1);
                    yield return new WaitForSeconds(1);
                }
                if (wave.waitOnTotalClear)
                {
                    spawner.SetActive(false);
                    while (FindObjectOfType<EnemyVision>())
                        yield return new WaitForSeconds(1);

                    // do between rounds upgrade

                    spawner.SetActive(true);
                }

                ++waveIndex;
            }
            waveIndex = 0;
        }
        while (loopWaves);

        yield return null;
    }

}
