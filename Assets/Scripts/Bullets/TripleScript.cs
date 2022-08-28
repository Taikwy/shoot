using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleScript : BulletScript
{
    float maxDistance;

    public void SetData(bool isEnemy, Vector2 moveDir, float maxDist){
        base.SetData(isEnemy, moveDir);
        maxDistance = maxDist;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(Vector2.Distance(bulletTransform.position, initialPosition) >= maxDistance){
            gameObject.SetActive(false);
        }
    }
}
