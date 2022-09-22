using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGun : Gun
{
    [Header("reloading Stats")]
    public float maxAmmo;
    [HideInInspector] public float currentAmmo;
    public float ammoRechargeRate;
    public float subRechargeRate;
    public float ammoRechargePause;
    public float ammoRechargeAmount;

    [Header("special gun info")]
    public List<GameObject> bulletPrefabs = new List<GameObject>();
    protected List<SpecialGun> bulletScripts = new List<SpecialGun>();
    public int currentBulletIndex = 0;
    [HideInInspector] public bool equipped = false;

    public override void Start()
    {
        foreach(GameObject bullet in bulletPrefabs){
            bulletScripts.Add(bullet.GetComponent<SpecialGun>());
        }
        currentBulletPrefab = bulletPrefabs[0];
        CreateBulletPool();
    }
    
    public override void CreateBulletPool(){
        foreach(GameObject bullet in bulletPrefabs){
            PoolManager.Instance.CreatePool(bullet, 50, "bullet");
        }
    }

    public override void Shoot()
    {
        currentAmmo -= ammoCost;
        isRecharging = false;
        timeSinceShot = 0;
    }

    public void Update(){
        Debug.Log("updating");
        timeSinceShot += Time.deltaTime;
        if(timeSinceShot >= ammoRechargePause)
            isRecharging = true;

        if(isRecharging && currentAmmo < maxAmmo){
            if(equipped)
                currentAmmo += ammoRechargeRate*Time.deltaTime;
            else
                currentAmmo += subRechargeRate*Time.deltaTime;

            if(currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;
        }
    }

    public override void RefillAmmo(float refillAmount = 0){
        currentAmmo += refillAmount;
        if(currentAmmo > maxAmmo)
            currentAmmo = maxAmmo;
    }
}
