using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSequence : AttackSequence
{

    [Header("Circle Info")]
    public bool centered;
    public float posOffset;

    public override void Shoot(){
        ShootForward();

        base.Shoot();
    }

    public void ShootForward(){
        GameObject bullet;
        Quaternion angleDeltaR = Quaternion.identity;

        float angleBetween = 360.0f/numBullets;

        for(int i = 0; i < numBullets; i++){
            if(centered){
                angleDeltaR = Quaternion.AngleAxis(angleBetween*i, firingPoint.forward);
            }
            else{
                angleDeltaR = Quaternion.AngleAxis(angleBetween*i + angleBetween/2, firingPoint.forward);
            }

            bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position + posOffset * (angleDeltaR * firingPoint.up), angleDeltaR);
            bullet.GetComponent<BulletScript>().SetData(true, angleDeltaR * firingPoint.up);
        }
    }

    public void ShootAt(){}

    public void ShootAtPlayer(){}
}
