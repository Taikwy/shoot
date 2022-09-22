using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGGun : PrimaryGun
{
    [Header("SMG Gun Info")]
    public float spread;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    public override void CreateBulletPool(){
        PoolManager.Instance.CreatePool(currentBulletPrefab, 100, "bullet");
    }

    public override void Shoot(){
        if(overheated || timeSinceShot < 1/roundsPerSecond){
            return;
        }

        Vector2 spawnPos = new Vector2(firingPoints[0].position.x + Random.Range(-spread/2, spread/2), firingPoints[0].position.y);
        GameObject bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, spawnPos, firingPoints[0].rotation);
        bullet.GetComponent<BulletScript>().SetData(false, firingPoints[0].up, range);
        
        base.Shoot();
    }
}
