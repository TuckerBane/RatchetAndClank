using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHealthbarLogic : MonoBehaviour
{
    public GameObject heart;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(heart != null, "HUDHealtbar needs an icon");

        var playerHealth = FindObjectOfType<IsPlayer>().GetComponent<Health>();
        var canvasTransform = transform.parent.GetComponent<RectTransform>();
        Vector3 iconSize = heart.GetComponent<RectTransform>().sizeDelta;
        Vector3 curPos = canvasTransform.position.Times(-0.25f) + iconSize.Times(0.25f);
        curPos = Vector3.zero;
        for(int i = 0; i < playerHealth.startingHealth; ++i)
        {
            GameObject newHeart = Instantiate(heart, this.GetComponent<RectTransform>());
            newHeart.GetComponent<RectTransform>().position = curPos;
            curPos.x = curPos.x + iconSize.x;
        }

    }

    // Update is called once per frame
    void Update()
    {
        var playerHealth = FindObjectOfType<IsPlayer>().GetComponent<Health>();
        int curHealth = playerHealth.health;
        int maxHealth = playerHealth.startingHealth;
        for(int i = 0; i < maxHealth; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(i < curHealth);
        }

    }
}
