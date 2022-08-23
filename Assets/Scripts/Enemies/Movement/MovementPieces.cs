using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPieces : MonoBehaviour
{
    [Header("Movement Component Info")]
    [SerializeField]
    private Rigidbody2D rb;
    public Transform t;
    public Vector2 newPosition;

    public Vector2 lastPosition;
    //Piece Stats -------------------------------
    public float pieceTime, pieceDist, pieceDistX, pieceDistY;

    Vector2 startPosition;

    [Header("Line Piece Data")]
    //Line Info ==================================================================================================
    LineData lineData;
    Vector2 lineDirection;


    [Header("Curve Piece Data")]
    //Curve info ==================================================================================================
    CurveData curveData;
    float radius, startingAngle;
    bool clockwise, mirrored;
    Vector2 rotationCenter;
    float currentAngle, currentRad;                 //Actual angle based off center
    public float deltaAngle, deltaRad;            //How far its curved
    int rotationDir, numRevolutions;

    [Header("Sin Piece Data")]
    //Curve info ==================================================================================================
    SinData sinData;
    Vector2 moveDirection;
    float amplitude, frequency, halfCycleDist;
    public int numHalfCycles;

    //resets everything thats tracking the pieces
    // public void ResetAll(){
    //     ResetPiece();
    //     newPosition = lastPosition;
    // }

    //resets just the stuff for a new piece to be used
    public void ResetPiece(){
        // lastPosition = rb.position;
        lastPosition = t.position;
        pieceTime = pieceDist = pieceDistX = pieceDistY = 0;
        startPosition = t.position;
        // Debug.Log("setting up" + t.position + " " +  rb.position + " " + lastPosition);
    }

    public void SetupStop(){
        ResetPiece();
    }

    public void SetupLine(LineData data = null, bool m = false){
        ResetPiece();
        mirrored = m;
        lineDirection = new Vector2((float)Mathf.Cos(data.direction*Mathf.Deg2Rad), (float)Mathf.Sin(data.direction*Mathf.Deg2Rad));
        if(mirrored)
            lineDirection.x *= -1;
    }

    public void SetupCurve(CurveData data = null, bool m = false){
        ResetPiece();

        radius = data.radius;
        startingAngle = data.startingAngle;
        clockwise = data.clockwise;
        mirrored = m;

        if(clockwise) rotationDir = -1;
        else rotationDir = 1;
        numRevolutions = 0;

        if(mirrored){
            clockwise = !clockwise;
            rotationDir *= -1;
            float flipAngleDifference = 180 - startingAngle;
            if(flipAngleDifference < 0)
                startingAngle = 360 + flipAngleDifference;
            else
                startingAngle = flipAngleDifference;

            // startingAngle = (startingAngle + 180)%360;

            float reverseAngle = (startingAngle + 180)%360;
            Vector2 reverseDirection = new Vector2(Mathf.Cos(reverseAngle * Mathf.Deg2Rad), Mathf.Sin(reverseAngle * Mathf.Deg2Rad));
            rotationCenter = (Vector2)t.position + radius * reverseDirection.normalized;
            Debug.Log("mirrored "+ rotationCenter + " " + reverseDirection + " " + reverseAngle + " " + startingAngle);
        }
        else{
            float reverseAngle = (startingAngle + 180)%360;
            Vector2 reverseDirection = new Vector2(Mathf.Cos(reverseAngle * Mathf.Deg2Rad), Mathf.Sin(reverseAngle * Mathf.Deg2Rad));
            rotationCenter = (Vector2)t.position + radius * reverseDirection.normalized;
        }
        
        currentAngle = startingAngle;
        currentRad = currentAngle * Mathf.Deg2Rad;
        deltaAngle = deltaRad = 0;
    }

    public void SetupSin(SinData data = null, bool m = false){
        ResetPiece();
        // startPosition = rb.position;
        moveDirection = new Vector2(0,-1);
        amplitude = data.amplitude;
        frequency = data.frequency;
        halfCycleDist = (1/frequency) * Mathf.PI;
        numHalfCycles = 0;
    }

    public void UpdatePiece(){
        pieceTime += Time.deltaTime;

        // pieceDist += Vector2.Distance(rb.position, lastPosition);
        pieceDist += Vector2.Distance(t.position, lastPosition);
        // if(Vector2.Distance(t.position, lastPosition) >= 1)
        //     Debug.Log("new mother fucker" + t.position + " " + lastPosition);
        pieceDistX += Mathf.Abs(t.position.x - lastPosition.x);
        pieceDistY += Mathf.Abs(t.position.y - lastPosition.y);

        lastPosition = t.position;
    }

    public Vector2 StopMovement(){
        newPosition = rb.position;
        UpdatePiece();
        return newPosition;
    }
    public Vector2 LineMovement(float speed){
        newPosition = rb.position + lineDirection * speed * Time.deltaTime;
        UpdatePiece();
        return newPosition;
    }
    public Vector2 CurveMovement(float speed){
        float x = rotationCenter.x + Mathf.Cos(currentRad) * radius;
        float y = rotationCenter.y + Mathf.Sin(currentRad) * radius;
        newPosition = new Vector2(x,y);
        // if(mirrored)
        //     newPosition.x = startPosition.x - newPosition.x;

        currentRad += rotationDir * (speed * Time.deltaTime / radius);
        deltaRad += rotationDir * (speed * Time.deltaTime / radius);
        currentAngle = currentRad * Mathf.Rad2Deg;
        deltaAngle = deltaRad * Mathf.Rad2Deg;
        
        if(currentAngle >= 360f){
            currentRad = 0f;
            currentAngle = 0f;
        }
        if(deltaAngle >= 360f){
            deltaRad = 0f;
            deltaAngle = 0f;
            numRevolutions++;
        }
        
        UpdatePiece();
        return newPosition;
    }

    public void CurveRotate(){
        if(clockwise)
            t.up = Vector2.Perpendicular(newPosition - rotationCenter);
        else
            t.up = Vector2.Perpendicular(rotationCenter - newPosition);
    }

    //Oscillates along X axis, moving in Y axis
    public Vector2 SinXMovement(float speed){
        float distanceTravelled = Mathf.Abs(startPosition.y - rb.position.y);

        newPosition = rb.position + moveDirection * speed * Time.deltaTime;
        newPosition.x  = startPosition.x + amplitude * Mathf.Sin(distanceTravelled * frequency);

        UpdatePiece();
        numHalfCycles = (int)(pieceDistY/halfCycleDist);

        return newPosition;
    }

    public Vector2 ClampSinX(){
        newPosition.x  = startPosition.x;
        t.position = newPosition;
        return newPosition;
    }

    //Oscillates along Y axis, moving in X axis
    public Vector2 SinYMovement(float speed){
        float distanceTravelled = Mathf.Abs(startPosition.y - rb.position.y);

        newPosition = rb.position + moveDirection * speed * Time.deltaTime;
        newPosition.y  = amplitude * Mathf.Sin(distanceTravelled * frequency);

        numHalfCycles = (int)(pieceDistX/halfCycleDist);

        UpdatePiece();
        return newPosition;
    }
}
