using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingA : SpecialGun
{
    [Header("multi shot data")]
    public int numShots;
    public float gapBetween;

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
        Debug.Log("alternating");
        if(ammoCost > currentAmmo || timeSinceShot < cooldown){
            return;
        }

        GameObject bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(false, firingPoint.up);

        currentBulletIndex = (currentBulletIndex+1) % bulletPrefabs.Count;
        currentBulletPrefab = bulletPrefabs[currentBulletIndex];
        // currentBulletScript = bulletScripts[currentBulletIndex];
        
        currentAmmo -= ammoCost;
        isRecharging = false;
        timeSinceShot = 0;
    }
}
