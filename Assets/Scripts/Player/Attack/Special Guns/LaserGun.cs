using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : SpecialGun
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        currentAmmo = maxAmmo;
    }

    public override void Shoot(){
        if(ammoCost > currentAmmo || timeSinceShot < 1/roundsPerSecond){
            return;
        }
        
        GameObject bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, firingPoints[0].position, firingPoints[0].rotation);
        bullet.GetComponent<BulletScript>().SetData(false, firingPoints[0].up);
        
        base.Shoot();
    }
}
