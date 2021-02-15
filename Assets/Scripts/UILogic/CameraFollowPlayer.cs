using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

    GameObject player;
    public float followDistance = 5.0f;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<IsPlayer>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 myFlatPos = transform.position;
        myFlatPos.z = 0;
        Vector3 toPlayer = (player.transform.position - myFlatPos);
        toPlayer.z = 0;
        if (toPlayer.magnitude > followDistance)
        {
            transform.position += toPlayer.normalized * (toPlayer.magnitude - followDistance);
        }

	}
}
