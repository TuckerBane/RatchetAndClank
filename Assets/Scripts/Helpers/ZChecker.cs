using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform tran in FindObjectsOfType<Transform>())
        {
            if(Mathf.Abs(tran.position.z) > 0.1f && !tran.gameObject.TryGetComponent<IsZExempt>(out IsZExempt z) && tran.gameObject.GetComponentInParent<IsZExempt>() == null)
            {
                Debug.Log("This game object has a non-zero z value and no IsZExempt component", tran.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
