using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadSequence : AttackSequence
{
    [Header("Spread Info")]
    public float angleBetween;
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
                float playerDirection = Vector2.SignedAngle(firingPoint.up, (Vector2)playerTransform.position - (Vector2)firingPoint.position);
                if(requireLOS && Mathf.Abs(playerDirection) > 90)
                    return;
                ShootAtPlayer();
                break;
        }

        base.Shoot();
    }

    public void ShootForward(){
        GameObject bullet;
        Quaternion angleDelta;
        float currentAngle = -1 * (numBullets-1)/2 * angleBetween;
        float currentXOFfset = -1 * (numBullets-1)/2 * xOffset;

        for(int i = 0; i < numBullets; i++){
            angleDelta = Quaternion.AngleAxis(currentAngle, firingPoint.forward);
            bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position + new Vector3(currentXOFfset, 0, 0), firingPoint.rotation);
            bullet.GetComponent<BulletScript>().SetData(true, angleDelta * firingPoint.up);

            currentAngle += angleBetween;
            currentXOFfset += xOffset;
        }
    }

    public void ShootAt(){}

    public void ShootAtPlayer(){
        GameObject bullet;
        Quaternion angleDelta;
        float playerDirection = Vector2.SignedAngle(firingPoint.up, (Vector2)playerTransform.position - (Vector2)firingPoint.position);
        float currentAngle = -1 * (numBullets-1)/2 * angleBetween + playerDirection;
        float currentXOFfset = -1 * (numBullets-1)/2 * xOffset;

        for(int i = 0; i < numBullets; i++){
            angleDelta = Quaternion.AngleAxis(currentAngle, firingPoint.forward);
            bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position + new Vector3(currentXOFfset, 0, 0), angleDelta);
            bullet.GetComponent<BulletScript>().SetData(true, angleDelta * firingPoint.up);

            currentAngle += angleBetween;
            currentXOFfset += xOffset;
        }
    }
}
