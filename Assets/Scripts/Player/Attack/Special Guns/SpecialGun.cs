using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGun : Gun
{
    [Header("special gun info")]
    public List<GameObject> bulletPrefabs = new List<GameObject>();
    protected List<SpecialGun> bulletScripts = new List<SpecialGun>();
    public int currentBulletIndex = 0;

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

    public virtual void RechargeInactiveAmmo(){
        if(isRecharging && currentAmmo < maxAmmo){
            currentAmmo += subRechargeRate*Time.deltaTime;
            if(currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;
        }
        timeSinceShot += Time.deltaTime;
        if(timeSinceShot >= ammoRechargePause)
            isRecharging = true;
    }

    // public virtual void RechargeAmmo(){
    //     if(isRecharging && currentAmmo < maxAmmo){
    //         currentAmmo += ammoRechargeRate*Time.deltaTime;
    //         if(currentAmmo > maxAmmo)
    //             currentAmmo = maxAmmo;
    //     }
    //     timeSinceShot += Time.deltaTime;
    //     if(timeSinceShot >= ammoRechargePause)
    //         isRecharging = true;
    // }

    // public virtual void RefillAmmo(float ammoAmount = 0){
    //     currentAmmo += ammoAmount;
    //     if(currentAmmo > maxAmmo)
    //         currentAmmo = maxAmmo;
    // }

    // public virtual void Shoot(){}
}
