using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LingeringBulletMod
{

    public GameObject mod;
    public Timer time;
    public GameObject iconArchetype;
    public GameObject iconInstance;

    public LingeringBulletMod(GameObject mod, Timer time, GameObject iconArchetype = null)
    {
        this.mod = mod;
        this.time = time;
        this.iconArchetype = iconArchetype;
    }
}

public class PlayerShooting : MonoBehaviour {

    private MouseCursor theMouse;
    private int currentBulletIndex = 0;
    public float timeOfLastShot = 0;
    public float baseShotCooldown = 0.5f;
    public float shotCooldown { get => baseShotCooldown * currentEffects.fireRateMod; }
    public WeaponVersionTracker versionTracker;
    public List<LingeringBulletMod> lingeringEffects;
    public GameObject bullet;
    public GameObject baseMode;
    public Timer weaponSwapCooldown;
    public float lingeringEffectDuration = 3.0f;
    public bool allowMultipleLingeringEffects = false;
    public bool allowWeaponSwap = false;
    public int clipSize = 8;
    public int currentShotsInClip = 8;
    public bool reloadInProgress = false;
    public Timer reloadCooldown;
    public bool applyWeaponSpread = true;
    public float maxMissDegrees = 10.0f;
    public AudioClip shootSound;
    public AudioClip reloadSound;

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
        if(allowWeaponSwap)
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
        if(baseMode)
            Instantiate(baseMode).transform.parent = bull.transform;

        foreach (LingeringBulletMod effect in lingeringEffects)
        {
            if (!effect.time.isDone())
                Instantiate(effect.mod).transform.parent = bull.transform;
            else
            {
                CleanupLingeringEffect(effect);
                toRemove.Add(effect);
            }
        }
        lingeringEffects = lingeringEffects.FindAll(effect => { return !toRemove.Contains(effect); });
        return bull;
    }

    public void AddLingeringEffect(LingeringBulletMod timedMod)
    {
        if (timedMod != null)
        { // some weapons may not have a lingering effect
            if (!allowMultipleLingeringEffects)
                lingeringEffects.Clear();

            var newModeType = timedMod.mod.GetComponent<BulletComponentBase>().GetType();
            for(int i = 0; i < lingeringEffects.Count; ++i)
            {
                var oldModeType = lingeringEffects[i].mod.GetComponent<BulletComponentBase>().GetType();
                if (oldModeType == newModeType)
                    RemoveLingeringEffectFromList(lingeringEffects[i]);
            }

            AddLingeringEffectToList(timedMod);
        }
        else
            Debug.Log("tried to add a null weapon mod. That's no good. PlayerShooting.cs");
    }

    public void SwitchToWeapon(int bulletIndex)
    {
        LingeringBulletMod newMod = new LingeringBulletMod(versionTracker.weaponVersions[currentBulletIndex].array[(int)PowerLevel.Lesser], new Timer(lingeringEffectDuration) );
        if (newMod.mod)
        { // some weapons may not have a lingering effect
            AddLingeringEffect(newMod);
        }
        else
            Debug.Log(((BulletIndex)bulletIndex).ToString() + "doesn't have a lingering effect. Just saying, maybe it should?");

        currentBulletIndex = bulletIndex;
        currentEffects = defualtEffects;
        bullet = versionTracker.weaponVersions[bulletIndex].bulletBase;
        baseMode = versionTracker.weaponVersions[bulletIndex].array[(int)PowerLevel.Normal];
        // can't send messages to archetypes
        // maybe do this on shoot instead if this caused problems
        GameObject tempBul = GetUninitializedBullet();
        foreach (BulletComponentBase comp in tempBul.GetComponentsInChildren<BulletComponentBase>())
            comp.InitializeWeapon(gameObject);
        DestroyImmediate(tempBul, false);
    }

	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0)
            return;

        if (allowWeaponSwap)
        {
            for (int i = 0; i < versionTracker.weaponVersions.Length; ++i)
            {
                if (i != currentBulletIndex && Input.GetKeyDown(KeyCode.Alpha1 + i) && weaponSwapCooldown.resetIfDone())
                {
                    SwitchToWeapon(i);
                }
            }
        }

        GetComponent<RotationHandler>().AddOrUpdateRotation("PlayerShooting", theMouse.VecToMouse(gameObject).AsRotation2d());

        if(currentShotsInClip == 0)
        {
            if (!reloadInProgress)
            {
                reloadInProgress = true;
                reloadCooldown.reset();
                GetComponent<AudioSource>().PlayOneShot(reloadSound);
            }
        }

        if (!reloadInProgress)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloadInProgress = true;
                reloadCooldown.reset();
                GetComponent<AudioSource>().PlayOneShot(reloadSound);
            }
        }
        else if (reloadCooldown.isDone())
        {
            reloadInProgress = false;
            currentShotsInClip = clipSize;
        }

        if (currentShotsInClip != 0 && !reloadInProgress && timeOfLastShot + shotCooldown <= Time.time && Input.GetMouseButton(0))
        {
            --currentShotsInClip;
            Vector3 toMouse = theMouse.VecToMouse(gameObject);
            if (applyWeaponSpread && timeOfLastShot + shotCooldown * 2 > Time.time)
            {
                float extraTimeSinceLastShot = Time.time - timeOfLastShot - shotCooldown;
                float curRecoilDegrees = maxMissDegrees * 1 - (extraTimeSinceLastShot / shotCooldown);
                toMouse = toMouse.Rotate2D(Random.Range(-curRecoilDegrees, curRecoilDegrees));
            }
            timeOfLastShot = Time.time;
            GameObject bull = GetUninitializedBullet();

            bull.GetComponent<BulletLogic>().Initialize(transform.position, toMouse, gameObject);

            foreach (BulletComponentBase comp in bull.GetComponentsInChildren<BulletComponentBase>())
                comp.InitializeWeaponComponents();

            // shot feedback
            FindObjectOfType<RFX4_CameraShake>().MyPlayShake();
            GetComponent<FakeRigidBody>().AddEffect(new MovementEffect(-toMouse, 0.1f, id: "shotPushback"));
            GetComponent<AudioSource>().PlayOneShot(shootSound);
        }


	}

    void AddLingeringEffectToList(LingeringBulletMod mod)
    {
        if (mod.iconArchetype)
        {
            mod.iconInstance = VisualsHelpers.AddFloatingIconToObject(mod.iconArchetype, gameObject, Vector3.back * 0.5f);
        }

        lingeringEffects.Add(mod);
        AdjustLingeringEffectPositions();
    }

    void RemoveLingeringEffectFromList(LingeringBulletMod mod)
    {
        CleanupLingeringEffect(mod);
        lingeringEffects.Remove(mod);
    }

    void RemoveAllLingeringEffects()
    {
        while (lingeringEffects.Count > 0)
            RemoveLingeringEffectFromList(lingeringEffects[0]);
    }

    void CleanupLingeringEffect(LingeringBulletMod mod)
    {
        if (mod.iconInstance)
        {
            Destroy(mod.iconInstance);
        }
        AdjustLingeringEffectPositions();
    }

    void AdjustLingeringEffectPositions()
    {
        for(int i = 0; i < lingeringEffects.Count; ++i)
        {
            if (!lingeringEffects[i].iconInstance)
                continue;

            Vector3 newPos = lingeringEffects[i].iconInstance.GetComponent<PositionOnlyParent>().localPosition;
            newPos.x = (lingeringEffects.Count / 2.0f - i - 0.5f) * 0.5f;
            lingeringEffects[i].iconInstance.GetComponent<PositionOnlyParent>().localPosition = newPos;
        }
    }
}
