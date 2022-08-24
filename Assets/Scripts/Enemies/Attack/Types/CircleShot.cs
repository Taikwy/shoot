using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShot : ShotType
{
    [System.Serializable]
    public struct Data{
        [Header("Shooting Type Info")]
        public Transform firingPoint;
        public GameObject bulletPrefab;
        public bool shootingForwards;
        public int numShots;
        public bool centered;

        [Header("Shooting Behavior Info")]
        public float shootRate;
        [HideInInspector]
        public float timer;        
    }

    public void SetupType(Data data){
        // base.SetupType(data.firingPoint);
    }

    public void ShootCircle(Data d){
        GameObject bullet;
        Quaternion angleDeltaR = Quaternion.identity;

        float angleBetween = 360.0f/d.numShots;

        for(int i = 0; i < d.numShots; i++){
            if(d.centered){
                angleDeltaR = Quaternion.AngleAxis(angleBetween*i, d.firingPoint.forward);
            }
            else{
                angleDeltaR = Quaternion.AngleAxis(angleBetween*i + angleBetween/2, d.firingPoint.forward);
            }

            bullet = PoolManager.Instance.ReuseObject(d.bulletPrefab, d.firingPoint.position, angleDeltaR);
            bullet.GetComponent<BulletScript>().SetData(true, angleDeltaR * -d.firingPoint.up);
        }
    }
}
