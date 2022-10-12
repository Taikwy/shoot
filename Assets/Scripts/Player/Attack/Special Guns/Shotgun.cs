using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : SpecialGun
{
    [Header("shotgun stuff")]
    public int numShots;
    public float spreadInAngles;
    public float range;

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
        float maxAngle = spreadInAngles/2;
        float minAngle = -spreadInAngles/2;
        Quaternion angleDelta;

        for(int i = 0; i < numShots; i++){
            float angle = Random.Range(maxAngle, minAngle);
            angleDelta = Quaternion.AngleAxis(angle, firingPoints[0].forward);
            bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, firingPoints[0].position, firingPoints[0].rotation);
            bullet.GetComponent<BulletScript>().SetData(false, angleDelta * firingPoints[0].up, range);
        }
        
        base.Shoot();
    }
}
