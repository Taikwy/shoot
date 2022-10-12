using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightSequence : AttackSequence
{
    [Header("Straight Info")]
    public float xOffset = 0;
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
        foreach(Transform fp in firingPoints){
            GameObject bullet;
            Vector3 positionDelta = Vector3.zero;
            float currentXOFfset = -1 * (numBullets-1)/2 * xOffset;

            for(int i = 0; i < numBullets; i++){
                bullet = PoolManager.Instance.ReuseObject(bulletPrefab, fp.position + new Vector3(currentXOFfset, 0, 0), fp.rotation);
                bullet.GetComponent<BulletScript>().SetData(true, fp.up);

                currentXOFfset += xOffset;
            }
        }
        
    }

    public void ShootAt(){}

    public void ShootAtPlayer(){
        float playerDirection = Vector2.SignedAngle(firingPoint.up, (Vector2)playerTransform.position - (Vector2)firingPoint.position);
        Quaternion angleDelta = Quaternion.AngleAxis(playerDirection, firingPoint.forward);

        GameObject bullet;
        Vector3 positionDelta = Vector3.zero;
        float currentXOFfset = -1 * (numBullets-1)/2 * xOffset;

        for(int i = 0; i < numBullets; i++){
            bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position + new Vector3(currentXOFfset, 0, 0), angleDelta);
            bullet.GetComponent<BulletScript>().SetData(true, angleDelta * firingPoint.up);

            currentXOFfset += xOffset;
        }
    }
}
