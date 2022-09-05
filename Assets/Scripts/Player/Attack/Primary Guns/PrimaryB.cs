using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryB : PrimaryGun
{
    public float shotXOffset;
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
        PoolManager.Instance.CreatePool(currentBulletPrefab, 100, "bullet");
    }

    public override void Shoot(){
        if(ammoCost > currentAmmo || timeSinceShot < cooldown){
            return;
        }
        Vector3 rightBulletPos = firingPoint.position + new Vector3(shotXOffset,0,0);
        Vector3 leftBulletPos = firingPoint.position - new Vector3(shotXOffset,0,0);
        Debug.Log(rightBulletPos + " " + leftBulletPos );

        GameObject bullet;
        bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, rightBulletPos, firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(false, firingPoint.up);

        bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, leftBulletPos, firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(false, firingPoint.up);
        
        currentAmmo -= ammoCost;
        isRecharging = false;
        timeSinceShot = 0;
    }
}
