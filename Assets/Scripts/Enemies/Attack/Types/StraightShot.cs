using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShot : ShotType
{
    [System.Serializable]
    public struct Data{
        [Header("Shooting Type Info")]
        public Transform firingPoint;
        public GameObject bulletPrefab;
        public bool shootingForwards;

        [Header("Shooting Behavior Info")]
        public float shootRate;
        [HideInInspector]
        public float timer;
    }

    public void SetupType(Data data){
        // base.SetupType(data.firingPoint);
    }

    public void ShootStraight(Data d){
        if(d.shootingForwards){
            shootDirection = -d.firingPoint.up;
        }
        else{
            shootDirection = d.firingPoint.up;
        }
        GameObject bullet = PoolManager.Instance.ReuseObject(d.bulletPrefab, d.firingPoint.position, d.firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(true, shootDirection);
    }

    public void ShootForward(Data data){
        GameObject bullet = PoolManager.Instance.ReuseObject(data.bulletPrefab, data.firingPoint.position, data.firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(true, -data.firingPoint.up);
    }

    public void ShootBackward(Data data){
        GameObject bullet = PoolManager.Instance.ReuseObject(data.bulletPrefab, data.firingPoint.position, data.firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(true, data.firingPoint.up);
    }
}
