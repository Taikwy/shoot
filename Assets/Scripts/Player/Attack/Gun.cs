using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("bullet info")]
    public Transform firingPoint;
    public GameObject bulletPrefab;
    public BulletScript bulletScript;

    [Header("reloading Stats")]
    public float maxAmmo;
    public float currentAmmo;
    public float ammoRechargePause;
    public float ammoRechargeRate;
    public float ammoRechargeAmount;

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
        bulletScript = bulletPrefab.GetComponent<BulletScript>();
        CreateBulletPool();
    }
    
    public void CreateBulletPool(){
        PoolManager.Instance.CreatePool(bulletPrefab, 100, "bullet");
    }

    public void RechargeAmmo(){
        if(isRecharging && currentAmmo < maxAmmo){
            currentAmmo += ammoRechargeRate*Time.deltaTime;
            if(currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;
        }
        timeSinceShot += Time.deltaTime;
            if(timeSinceShot >= ammoRechargePause)
                isRecharging = true;
    }

    public void RefillAmmo(float ammoAmount = 0){
        currentAmmo += ammoAmount;
        if(currentAmmo > maxAmmo)
            currentAmmo = maxAmmo;
    }

    public virtual void Shoot(){}
}
