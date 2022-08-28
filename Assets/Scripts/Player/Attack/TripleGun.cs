using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleGun : Gun
{
    [Header("triple shot data")]
    public int numShots = 3;
    public float gapBetween = 10;
    public float maxDistance = 10;

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
        if(bulletScript.ammoCost > currentAmmo || timeSinceShot < bulletScript.cooldown){
            return;
        }

        GameObject bullet;
        Quaternion angleDelta;
        float currentAngle = -1 * (numShots-1)/2 * gapBetween;

        for(int i = 0; i < numShots; i++){
            angleDelta = Quaternion.AngleAxis(currentAngle, firingPoint.forward);
            bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position, firingPoint.rotation);
            bullet.GetComponent<TripleScript>().SetData(false, angleDelta * firingPoint.up, maxDistance);

            Debug.Log(angleDelta + " " + i);

            currentAngle += gapBetween;
        }
        currentAmmo -= bulletScript.ammoCost;
        isRecharging = false;
        timeSinceShot = 0;
    }
}
