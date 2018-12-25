using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    private MouseCursor theMouse;
    public float timeOfLastShot = 0;
    public float shotCooldown = 0.5f;
    public GameObject bullet;
    public GameObject[] lingeringEffects;

    public struct TemporaryEffects
    {
        public float fireRateMod;
    }
    public TemporaryEffects defualtEffects;
    public TemporaryEffects currentEffects;

	// Use this for initialization
	void Start () {
        theMouse = FindObjectOfType<MouseCursor>();
	}
	
    public void SwapWeapons()
    {
        currentEffects = defualtEffects;
    }

    private void CopyFields(object source, object destination)
    {
        System.Reflection.FieldInfo[] fields = source.GetType().GetFields();
        foreach (System.Reflection.FieldInfo fieldInfo in fields)
        {
            if (!fieldInfo.FieldType.IsClass)
                fieldInfo.SetValue(destination, fieldInfo.GetValue(source));
            else
                Debug.LogWarning(fieldInfo.ToString() + " is a class field");
        }
    }

    public GameObject GetUninitializedBullet()
    {
        GameObject bull = Instantiate(bullet, transform.position, transform.rotation);
        foreach(GameObject effect in lingeringEffects)
        {
            Instantiate(effect).transform.parent = bull.transform;
        }
        return bull;
    }

	// Update is called once per frame
	void Update () {

        GetComponent<RotationHandler>().AddOrUpdateRotation("PlayerShooting",theMouse.VecToMouse(gameObject).AsRotation2d());

        if (timeOfLastShot + shotCooldown <= Time.time && Input.GetMouseButton(0))
        {
            timeOfLastShot = Time.time;
            Vector3 toMouse = theMouse.VecToMouse(gameObject);
            GameObject bull = GetUninitializedBullet();
            bull.GetComponent<BulletLogic>().Initialize(transform.position, toMouse, gameObject);
            bull.BroadcastMessage("InitializeWeaponComponents", SendMessageOptions.DontRequireReceiver);
        }
	}
}
