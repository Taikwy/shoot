using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMGGun : SpecialGun
{
    [Header("lmg gun stats")]
    public float spread;

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
        Vector2 spawnPos = new Vector2(firingPoints[0].position.x + Random.Range(-spread/2, spread/2), firingPoints[0].position.y);
        GameObject bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, spawnPos, firingPoints[0].rotation);
        bullet.GetComponent<BulletScript>().SetData(false, firingPoints[0].up);
        
        base.Shoot();
    }
}
