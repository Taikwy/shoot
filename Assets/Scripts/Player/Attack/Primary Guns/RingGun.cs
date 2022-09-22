using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGun : PrimaryGun
{
    [Header("Ring Gun Info")]
    public int numShots;
    //public bool centered;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        // currentAmmo = maxAmmo;
        currentHeat = 0;
    }

    public override void CreateBulletPool(){
        PoolManager.Instance.CreatePool(currentBulletPrefab, 100, "bullet");
    }

    public override void Shoot(){
        if(overheated || timeSinceShot < 1/roundsPerSecond){
            return;
        }

        GameObject bullet;
        Quaternion angleDeltaR = Quaternion.identity;

        float angleBetween = 360.0f/numShots;

        for(int i = 0; i < numShots; i++){
            angleDeltaR = Quaternion.AngleAxis(angleBetween*i, firingPoints[0].forward);
            // if(!centered)
            //     angleDeltaR = Quaternion.AngleAxis(angleBetween*i + angleBetween/2, firingPoint.forward);

            bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, firingPoints[0].position, angleDeltaR);
            bullet.GetComponent<BulletScript>().SetData(false, angleDeltaR * firingPoints[0].up);
        }
        
        base.Shoot();
    }

}
