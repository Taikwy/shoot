using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsSequence : AttackSequence
{
    public enum DIRECTION{
        FORWARD,
        POSITION,
        PLAYER
    }
    public DIRECTION direction;
    

    public override void Shoot(){
        switch(direction){
            case DIRECTION.FORWARD:
                ShootForward();
                break;
            case DIRECTION.POSITION:
                ShootAt();
                break;
            case DIRECTION.PLAYER:
                ShootAtPlayer();
                break;
        }
        base.Shoot();
    }


    public void ShootForward(){
    }

    public void ShootAt(){
        GameObject bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(true, firingPoint.up);
    }

    public void ShootAtPlayer(){
        float angle = Vector2.SignedAngle(firingPoint.up, (Vector2)playerTransform.position - (Vector2)firingPoint.position);
        Quaternion angleDelta = Quaternion.AngleAxis(angle, firingPoint.forward);

        GameObject bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position, angleDelta);
        bullet.GetComponent<BulletScript>().SetData(true, angleDelta * firingPoint.up);
    }
}
