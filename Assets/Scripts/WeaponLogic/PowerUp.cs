using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject mod;
    public float duration = 20.0f;
    public float spinSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, spinSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerShooting playerShooting))
        {
            playerShooting.AddLingeringEffect(new LingeringBulletMod(mod, new Timer(duration), GetComponent<AddIcon>()?.IconArchetype));
            Destroy(gameObject);
        }
    }

}
