using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : BulletScript
{
    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        transform.localScale = Vector3.one;
    }

    protected override void FixedUpdate()
    {
        transform.localScale += Vector3.one * .1f;
        base.FixedUpdate();
    }
}
