using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadGun : SpecialGun
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
        
        GameObject bullet;
        for(int i = 0; i < 4; i++){
            bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, firingPoints[i].position, firingPoints[i].rotation);
            bullet.GetComponent<BulletScript>().SetData(false, firingPoints[i].up);
        }
        
        base.Shoot();
    }
}
