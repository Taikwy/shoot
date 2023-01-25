using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyMovement : MovementSequence
{
    public override void SetupPath(int startingPathIndex = 0){}
    public override void SetPath(){}

    public override Vector2 Move(float speed){
        UpdateSequence();
        return rb.position;
    }

    //Updates stats based on newposition
    public override void UpdateSequence(){
        time += Time.deltaTime;
        distance += Vector2.Distance(newPosition, objectTransform.position);
    }
}
