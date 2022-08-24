using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : ShotType
{
    [System.Serializable]
    public struct Data{
        [Header("Shooting Type Info")]
        public Transform firingPoint;
        public GameObject bulletPrefab;
        public bool shootingForwards;
        public int numShots;
        public float gapBetween;

        [Header("Shooting Behavior Info")]
        public float shootRate;
        [HideInInspector]
        public float timer;
    }

    public void SetupType(Data data){
        // base.SetupType(data.firingPoint);
        if(data.shootingForwards){
            shootDirection = -data.firingPoint.up;
        }
        else{
            shootDirection = data.firingPoint.up;
        }
    }

    public void ShootSpread(Data d){
        if(d.shootingForwards){
            shootDirection = -d.firingPoint.up;
        }
        else{
            shootDirection = d.firingPoint.up;
        }
        GameObject bullet;
        Quaternion angleDelta;
        float currentAngle = -1 * (d.numShots-1)/2 * d.gapBetween;

        for(int i = 0; i < d.numShots; i++){
            angleDelta = Quaternion.AngleAxis(currentAngle, d.firingPoint.forward);
            bullet = PoolManager.Instance.ReuseObject(d.bulletPrefab, d.firingPoint.position, d.firingPoint.rotation);
            bullet.GetComponent<BulletScript>().SetData(true, angleDelta * shootDirection);

            currentAngle += d.gapBetween;
        }
    }

    public void ShootTriple(Data d){
        GameObject bullet;
        Quaternion angleDeltaL = Quaternion.AngleAxis(-15, d.firingPoint.forward);
        Quaternion angleDeltaR = Quaternion.AngleAxis(15, d.firingPoint.forward);

        bullet = PoolManager.Instance.ReuseObject(d.bulletPrefab, d.firingPoint.position, d.firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(true, -d.firingPoint.up);

        bullet = PoolManager.Instance.ReuseObject(d.bulletPrefab, d.firingPoint.position, angleDeltaL);
        bullet.GetComponent<BulletScript>().SetData(true, angleDeltaL * -d.firingPoint.up);

        bullet = PoolManager.Instance.ReuseObject(d.bulletPrefab, d.firingPoint.position, angleDeltaR);
        bullet.GetComponent<BulletScript>().SetData(true, angleDeltaR * -d.firingPoint.up);
    }
}
