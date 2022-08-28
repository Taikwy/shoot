using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : Gun
{

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        maxAmmo = 100;
        currentAmmo = maxAmmo;
        ammoRechargePause = 0.25f;
        ammoRechargeRate = 20;
        ammoRechargeAmount = 20;
    }

    // Update is called once per frame
    void Update()
    {
        base.RechargeAmmo();
    }

    public override void Shoot(){
        // Debug.Log(bulletScript.ammoCost + " " + currentAmmo + " " + timeSinceShot + " " + bulletScript.cooldown);
        if(bulletScript.ammoCost > currentAmmo || timeSinceShot < bulletScript.cooldown){
            return;
        }
        GameObject bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(false, firingPoint.up);
        
        currentAmmo -= bulletScript.ammoCost;
        isRecharging = false;
        timeSinceShot = 0;
        
    }
}
