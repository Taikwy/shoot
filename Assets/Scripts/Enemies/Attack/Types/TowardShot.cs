using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardShot : ShotType
{
    [System.Serializable]
    public struct Data{
        [Header("Shooting Type Info")]
        public Transform firingPoint;
        public GameObject bulletPrefab;
        public bool shootingForwards;
        public Vector2 targetPosition;

        [Header("Shooting Behavior Info")]
        public float shootRate;
        [HideInInspector]
        public float timer;
    }
    
    public void SetupType(Data data){
        // base.SetupType(data.firingPoint);
        playerTransform = SingletonManager.Instance.player.transform;
        if(data.shootingForwards){
            shootDirection = -data.firingPoint.up;
        }
        else{
            shootDirection = data.firingPoint.up;
        }
    }
    
    public void ShootTowards(Data d){
        if(d.shootingForwards){
            shootDirection = -d.firingPoint.up;
        }
        else{
            shootDirection = d.firingPoint.up;
        }
        float angle = Vector2.SignedAngle(shootDirection, d.targetPosition - (Vector2)d.firingPoint.position);
        Quaternion angleDelta = Quaternion.AngleAxis(angle, d.firingPoint.forward);

        GameObject bullet = PoolManager.Instance.ReuseObject(d.bulletPrefab, d.firingPoint.position, angleDelta);
        bullet.GetComponent<BulletScript>().SetData(true, angleDelta * shootDirection);
    }
    public void ShootTowardsPlayer(Data d){
        playerTransform = SingletonManager.Instance.player.transform;
        if(d.shootingForwards){
            shootDirection = -d.firingPoint.up;
        }
        else{
            shootDirection = d.firingPoint.up;
        }
        float angle = Vector2.SignedAngle(shootDirection, (Vector2)playerTransform.position - (Vector2)d.firingPoint.position);
        Quaternion angleDelta = Quaternion.AngleAxis(angle, d.firingPoint.forward);

        GameObject bullet = PoolManager.Instance.ReuseObject(d.bulletPrefab, d.firingPoint.position, angleDelta);
        bullet.GetComponent<BulletScript>().SetData(true, angleDelta * shootDirection);
    }
}
