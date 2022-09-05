using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotA : SpecialGun
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
        if(ammoCost > currentAmmo || timeSinceShot < cooldown){
            return;
        }

        GameObject bullet;
        Vector3 bulletPos;
        float xOffset = -1 * (float)(numShots-1.0f)/2.0f * gapBetween;

        for(int i = 0; i < numShots; i++){
            bulletPos = firingPoint.position - new Vector3(xOffset,0,0);
            bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, bulletPos, firingPoint.rotation);
            bullet.GetComponent<BulletScript>().SetData(false, firingPoint.up);

            xOffset += gapBetween;
        }
        
        currentAmmo -= ammoCost;
        isRecharging = false;
        timeSinceShot = 0;
    }
}
