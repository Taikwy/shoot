using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMovement : MovementPiece
{
    [Header("Line Info")]
    bool mirrored;
    Vector2 lineDirection;

    [System.Serializable]
    public struct LineData{
        public float direction;
        public float lineCondition;                                //actual amt for distance || time, probably just time for now actually
    }

    public void Setup(LineData data, bool m = false){
        ResetPiece();
        mirrored = m;
        lineDirection = new Vector2((float)Mathf.Cos(data.direction*Mathf.Deg2Rad), (float)Mathf.Sin(data.direction*Mathf.Deg2Rad));
        if(mirrored)
            lineDirection.x *= -1;
    }

    public Vector2 Movement(float speed){
        newPosition = rb.position + lineDirection * speed * Time.deltaTime;
        UpdatePiece();
        return newPosition;
    }
}
