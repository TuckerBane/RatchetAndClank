using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDeathHandler : MonoBehaviour
{
    public void Die(DamageInfo info)
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerShooting>().enabled = false;
        GetComponent<Health>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<MeshRenderer>().material.color = Color.red;
        StartCoroutine(DieVerySoon());
    }

    private IEnumerator DieVerySoon()
    {
        var player = FindObjectOfType<IsPlayer>().gameObject;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerShooting>().enabled = false;

        yield return new WaitForSeconds(2.0f);

        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}