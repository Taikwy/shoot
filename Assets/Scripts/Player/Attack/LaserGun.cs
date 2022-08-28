using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun
{
    [Header("laser shot data")]
    public float sizeIncreaseRate = .1f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        maxAmmo = 100;
        currentAmmo = maxAmmo;
        ammoRechargePause = 1;
        ammoRechargeRate = 10;
        ammoRechargeAmount = 15;
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
        bullet.GetComponent<LaserScript>().SetData(false, firingPoint.up, sizeIncreaseRate);
        
        currentAmmo -= bulletScript.ammoCost;
        isRecharging = false;
        timeSinceShot = 0;
    }
}
