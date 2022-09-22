using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefyShotA : SpecialGun
{
    [Header("beefy shot data")]
    public float sizeIncreaseRate;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        // maxAmmo = 100;
        // ammoRechargePause = 1;
        // ammoRechargeRate = 10;
        // ammoRechargeAmount = 15;
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        // base.RechargeAmmo();        
    }

    public override void Shoot(){
        if(ammoCost > currentAmmo || timeSinceShot < roundsPerSecond){
            return;
        }
        GameObject bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<LaserScript>().SetData(false, firingPoint.up, sizeIncreaseRate);
        
        currentAmmo -= ammoCost;
        isRecharging = false;
        timeSinceShot = 0;
    }
}
