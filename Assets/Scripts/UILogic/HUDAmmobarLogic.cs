using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDAmmobarLogic : MonoBehaviour
{

    public GameObject ammo;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(ammo != null, "HUDAmmobar needs an icon");

        var playerShooting = FindObjectOfType<PlayerShooting>();
        var canvasTransform = transform.parent.GetComponent<RectTransform>();
        Vector3 iconSize = ammo.GetComponent<RectTransform>().sizeDelta;
        Vector3 curPos = new Vector3(513 * 4 - (iconSize.x * 3.0f), iconSize.y * 0.5f, iconSize.z);
        for (int i = 0; i < playerShooting.clipSize; ++i)
        {
            GameObject newBullet = Instantiate(ammo, GetComponent<RectTransform>());
            newBullet.GetComponent<RectTransform>().position = curPos;
            curPos.x = curPos.x - iconSize.x;
        }

    }

    // Update is called once per frame
    void Update()
    {
        var playerShooting = FindObjectOfType<PlayerShooting>();
        int curBullets = playerShooting.currentShotsInClip;
        int maxBullets = playerShooting.clipSize;
        for (int i = 0; i < maxBullets; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(i < curBullets);
        }

    }

}
