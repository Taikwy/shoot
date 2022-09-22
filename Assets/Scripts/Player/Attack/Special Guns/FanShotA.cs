using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanShotA : SpecialGun
{
    [Header("fan shot data")]
    public int numShots;
    public float gapBetween;
    public float maxDistance;

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

        GameObject bullet;
        Quaternion angleDelta;
        float currentAngle = -1 * (float)(numShots-1.0f)/2.0f * gapBetween;

        for(int i = 0; i < numShots; i++){
            angleDelta = Quaternion.AngleAxis(currentAngle, firingPoint.forward);
            bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, firingPoint.position, firingPoint.rotation);
            bullet.GetComponent<TripleScript>().SetData(false, angleDelta * firingPoint.up, maxDistance);

            currentAngle += gapBetween;
        }
        currentAmmo -= ammoCost;
        isRecharging = false;
        timeSinceShot = 0;
    }
}
