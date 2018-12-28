using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LingeringBulletMod
{

    public GameObject mod;
    public Timer time;

    public LingeringBulletMod(GameObject mod, Timer time)
    {
        this.mod = mod;
        this.time = time;
    }
}

public class PlayerShooting : MonoBehaviour {

    private MouseCursor theMouse;
    private int currentBulletIndex = 0;
    public float timeOfLastShot = 0;
    public float shotCooldown = 0.5f;
    public WeaponVersionTracker versionTracker;
    public List<LingeringBulletMod> lingeringEffects;
    public GameObject bullet;
    public Timer weaponSwapCooldown;
    public float lingeringEffectDuration = 3.0f;
    public bool allowMultipleLingeringEffects = false;

    [System.Serializable]
    public struct TemporaryEffects
    {
        public float fireRateMod;
    }
    public TemporaryEffects defualtEffects;
    public TemporaryEffects currentEffects;

    private void Awake()
    {
        lingeringEffects = new List<LingeringBulletMod>();
    }

    // Use this for initialization
    void Start () {
        theMouse = FindObjectOfType<MouseCursor>();
        SwitchToWeapon(currentBulletIndex);
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
        GameObject bull = Instantiate(bullet, transform.position, bullet.transform.rotation);
        List<LingeringBulletMod> toRemove = new List<LingeringBulletMod>();
        foreach(LingeringBulletMod effect in lingeringEffects)
        {
            if (!effect.time.isDone())
                Instantiate(effect.mod).transform.parent = bull.transform;
            else
                toRemove.Add(effect);
        }
        lingeringEffects = lingeringEffects.FindAll(effect => { return !toRemove.Contains(effect); });
        return bull;
    }

    public void SwitchToWeapon(int bulletIndex)
    {
        LingeringBulletMod newMod = new LingeringBulletMod(versionTracker.weaponVersions[currentBulletIndex].array[(int)PowerLevel.Lesser], new Timer(lingeringEffectDuration) );
        if (newMod.mod) // some weapons may not have a lingering effect
            lingeringEffects.Add(newMod);
        else
            Debug.Log(((BulletIndexe)bulletIndex).ToString() + "doesn't have a lingering effect. Just saying, maybe it should?");

        currentBulletIndex = bulletIndex;
        currentEffects = defualtEffects;
        bullet = versionTracker.weaponVersions[bulletIndex].array[(int)PowerLevel.Normal];
        // can't send messages to archetypes
        // maybe do this on shoot instead if this caused problems
        GameObject tempBul = GetUninitializedBullet();
        tempBul.BroadcastMessage("InitializeWeapon", gameObject, SendMessageOptions.DontRequireReceiver);
        DestroyImmediate(tempBul, false);
    }

	// Update is called once per frame
	void Update () {
        for (int i = 0; i < versionTracker.weaponVersions.Length; ++i)
        {
            if (i != currentBulletIndex && Input.GetKeyDown(KeyCode.Alpha1 + i) && weaponSwapCooldown.resetIfDone())
            {
                SwitchToWeapon(i);
            }
        }

        GetComponent<RotationHandler>().AddOrUpdateRotation("PlayerShooting",theMouse.VecToMouse(gameObject).AsRotation2d());

        if (timeOfLastShot + shotCooldown * currentEffects.fireRateMod <= Time.time && Input.GetMouseButton(0))
        {
            timeOfLastShot = Time.time;
            Vector3 toMouse = theMouse.VecToMouse(gameObject);
            GameObject bull = GetUninitializedBullet();
            bull.GetComponent<BulletLogic>().Initialize(transform.position, toMouse, gameObject);
            bull.BroadcastMessage("InitializeWeaponComponents", SendMessageOptions.DontRequireReceiver);
        }
	}
}
