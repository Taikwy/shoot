using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryDouble : PrimaryGun
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    public override void CreateBulletPool(){
        PoolManager.Instance.CreatePool(currentBulletPrefab, 80, "bullet");
    }

    public override void Shoot(){
        if(overheated || timeSinceShot < 1/roundsPerSecond){
            return;
        }

        GameObject leftBullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, firingPoints[0].position, firingPoints[0].rotation);
        leftBullet.GetComponent<BulletScript>().SetData(false, firingPoints[0].up);

        GameObject rightBullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, firingPoints[1].position, firingPoints[1].rotation);
        rightBullet.GetComponent<BulletScript>().SetData(false, firingPoints[1].up);
                
        base.Shoot();
        
    }
}
