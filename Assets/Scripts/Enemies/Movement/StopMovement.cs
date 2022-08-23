using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovement : MovementPiece
{
    [System.Serializable]
    public struct StopData{
        public float stopTime;
    }

    public void Setup(LineData data, bool m = false){
        ResetPiece();
    }

    public Vector2 Movement(float speed){
        newPosition = rb.position;
        UpdatePiece();
        return newPosition;
    }
}
