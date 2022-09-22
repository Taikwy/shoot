using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryA : PrimaryGun
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        // currentAmmo = maxAmmo;
    }

    public override void CreateBulletPool(){
        PoolManager.Instance.CreatePool(currentBulletPrefab, 50, "bullet");
    }

    public override void Shoot(){
        if(timeSinceShot < roundsPerSecond){
            return;
        }
        GameObject bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(false, firingPoint.up);
        
        // currentAmmo -= ammoCost;
        isRecharging = false;
        timeSinceShot = 0;
        
    }
}
