using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleScript : BulletScript
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(Vector2.Distance(transform.position, initialPosition) >= 5){
            gameObject.SetActive(false);
        }
    }
}
