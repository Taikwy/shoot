using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : SpecialGun
{
    [Header("lmg gun stats")]
    public float spread;
    public float angleSpread;

    float maxSpread, maxAngle;
    float xOffset, angleOffset = 0;
    
    public override void Shoot(){

        xOffset = Random.Range(-spread/2, spread/2);
        if(playerData.isMoving)
            angleOffset = xOffset/(spread/2) * angleSpread/2;
        else
            angleOffset = 0;
            
        Debug.Log(angleOffset + " " );

        Quaternion angleDelta = Quaternion.AngleAxis(angleOffset, firingPoints[0].forward);
        Vector2 spawnPos = new Vector2(firingPoints[0].position.x + xOffset, firingPoints[0].position.y);
        GameObject bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, spawnPos, firingPoints[0].rotation);
        bullet.GetComponent<BulletScript>().SetData(false, angleDelta * firingPoints[0].up);
        
        base.Shoot();
    }
}
