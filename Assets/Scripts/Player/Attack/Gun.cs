using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("bullet info")]
    public Transform firingPoint;
    public GameObject currentBulletPrefab;
    // [HideInInspector]
    // public BulletScript currentBulletScript;

    [Header("reloading Stats")]
    public float maxAmmo;
    public float ammoRechargeRate;
    public float subRechargeRate;
    public float ammoRechargePause;
    public float ammoRechargeAmount;
    public float currentAmmo;

    [Header("Bullet Stats")]
    public int maxHealth;
    public int currentHealth;
    public float ammoCost;
    public float cooldown;

    [Header("gun shooting info")]
    public bool isRecharging = false;
    public float timeSinceShot = 0f;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // currentBulletScript = currentBulletPrefab.GetComponent<BulletScript>();
        CreateBulletPool();
    }
    
    public virtual void CreateBulletPool(){
        PoolManager.Instance.CreatePool(currentBulletPrefab, 100, "bullet");
    }

    public virtual void RechargeAmmo(){
        if(isRecharging && currentAmmo < maxAmmo){
            currentAmmo += ammoRechargeRate*Time.deltaTime;
            if(currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;
        }
        timeSinceShot += Time.deltaTime;
        if(timeSinceShot >= ammoRechargePause)
            isRecharging = true;
    }

    public virtual void RefillAmmo(float ammoAmount = 0){
        currentAmmo += ammoAmount;
        if(currentAmmo > maxAmmo)
            currentAmmo = maxAmmo;
    }

    public virtual void Shoot(){}
}
