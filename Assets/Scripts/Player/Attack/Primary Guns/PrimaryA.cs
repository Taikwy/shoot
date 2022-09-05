using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryA : PrimaryGun
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        // maxAmmo = 100;
        // ammoRechargePause = 0.25f;
        // ammoRechargeRate = 20;
        // ammoRechargeAmount = 20;
        currentAmmo = maxAmmo;
    }

    public override void CreateBulletPool(){
        PoolManager.Instance.CreatePool(currentBulletPrefab, 50, "bullet");
    }

    public override void Shoot(){
        if(ammoCost > currentAmmo || timeSinceShot < cooldown){
            return;
        }
        GameObject bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(false, firingPoint.up);
        
        currentAmmo -= ammoCost;
        isRecharging = false;
        timeSinceShot = 0;
        
    }
}
