using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class MoveRight : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Timeline.ActivationControlPlayable
        transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
    }
}
