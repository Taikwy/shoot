using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSequence : MovementSequence
{
    [Header("Line Info")]
    public bool mirrored;
    public Vector2 lineDirection;
    public Vector2 endPosition;
    // LineData data;

    public float direction;
    public float distanceToTravel;                                //actual amt for distance || time, probably just time for now actually
    public enum TYPE{
        single,
        pingpong,
        repeat
    }
    public TYPE type;

    public GameObject pointA, pointB;


    public override void Setup(GameObject gameObj){
        SetupSequenceInfo(gameObj);
        lineDirection = new Vector2((float)Mathf.Cos(direction*Mathf.Deg2Rad), (float)Mathf.Sin(direction*Mathf.Deg2Rad));
        
        endPosition = startPosition + distanceToTravel * lineDirection;
    }

    public override Vector2 MoveSequence(float speed){
        if(endReached){
            switch(type){
                case TYPE.single:
                    sequenceComplete = true;
                    break;
                case TYPE.pingpong:
                    lineDirection = new Vector2(lineDirection.x *= -1, lineDirection.y *= -1);
                    (startPosition, endPosition) = (endPosition, startPosition);
                    endReached = false;
                    ResetSequence();
                    break;
                case TYPE.repeat:
                    newPosition = startPosition;
                    endReached = false;
                    ResetSequence();
                    break;
            }
        }
        else{
            Movement(speed);
            UpdateSequence();
            if(currentDist >= distanceToTravel){
                newPosition = endPosition;
                endReached = true;
            }
        }

        return newPosition;
    }

    public Vector2 Movement(float speed){
        // Debug.Log(rb.position + " " + lineDirection + " " + speed + " " + Time.deltaTime);
        newPosition = rb.position + lineDirection * speed * Time.deltaTime;
        
        return newPosition;
    }
}
