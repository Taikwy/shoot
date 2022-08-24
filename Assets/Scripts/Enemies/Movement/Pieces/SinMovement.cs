using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MovementPiece
{
    [Header("Sin Info")]
    bool mirrored;
    Vector2 moveDirection;
    float amplitude, frequency, halfCycleDist;
    //Accessed by movement pattern to change pieces
    [HideInInspector]
    public int numHalfCycles;

    [System.Serializable]
    public struct SinData{
        public float amplitude, frequency;
        public int numHalfCycles;                                //based on number of half cycles
    }

    public void Setup(SinData data, bool m = false){
        SetupPiece();
        mirrored = m;

        moveDirection = new Vector2(0,-1);
        amplitude = data.amplitude;
        frequency = data.frequency;
        halfCycleDist = (1/frequency) * Mathf.PI;
        numHalfCycles = 0;
    }

    //Travels down while moving side to side
    public Vector2 MovementX(float speed){
        float distanceTravelled = Mathf.Abs(startPosition.y - rb.position.y);

        newPosition = rb.position + moveDirection * speed * Time.deltaTime;
        newPosition.x  = startPosition.x + amplitude * Mathf.Sin(distanceTravelled * frequency);

        UpdatePiece();
        numHalfCycles = (int)(pieceDistY/halfCycleDist);

        return newPosition;
    }

    public Vector2 ClampSinX(){
        newPosition.x  = startPosition.x;
        objectTransform.position = newPosition;
        return newPosition;
    }

    public Vector2 MovementY(float speed){
        float distanceTravelled = Mathf.Abs(startPosition.y - rb.position.y);

        newPosition = rb.position + moveDirection * speed * Time.deltaTime;
        newPosition.y  = amplitude * Mathf.Sin(distanceTravelled * frequency);

        numHalfCycles = (int)(pieceDistX/halfCycleDist);

        UpdatePiece();
        return newPosition;
    }
}
