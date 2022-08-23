using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovementPiece : MonoBehaviour
{
    [Header("Movement Component Info")]
    [SerializeField]
    private Rigidbody2D rb;
    public TestMovementPattern tmp;
    public string currentPiece;
    public Vector2 newPosition;

    //Piece Stats -------------------------------
    public Vector2 lastPosition;
    public float pieceTime, pieceDist, pieceDistX, pieceDistY;


    [Header("Line Piece Data")]
    //Line Info ==================================================================================================
    [SerializeField]
    LineData lineData;

    //Line Stats -----------------------------
    public Vector2 lineDirection;


    [Header("Curve Piece Data")]
    //Curve info ==================================================================================================
    [SerializeField]
    CurveData curveData;

    float radius, startingAngle;
    //Curve Stats -----------------------------
    Vector2 rotationCenter;
    float currentAngle;                 //Actual angle based off center
    public float deltaAngle;            //How far its curved
    float currentRad;
    public float deltaRad;
    public int rotationDir;
    int numRevolutions;

    [Header("Sin Piece Data")]
    //Curve info ==================================================================================================
    [SerializeField]
    SinData sinData;
    public Vector2 startPosition;
    Vector2 moveDirection;
    float amplitude;
    public float frequency;
    public float halfCycleDist;
    public int numHalfCycles;

    public void Reset(){
        lastPosition = rb.position;
        pieceTime = pieceDist = pieceDistX = pieceDistY = 0;
    }
    public void SetupStop(){
        Reset();
    }

    public void SetupLine(LineData data = null){
        Reset();
        //Line movement info -----------------------------
        lineDirection = new Vector2((float)Mathf.Cos(data.direction*Mathf.Deg2Rad), (float)Mathf.Sin(data.direction*Mathf.Deg2Rad));
    }

    public void SetupCurve(CurveData data = null){
        Reset();

        radius = data.radius;
        startingAngle = data.startingAngle;

        float reverseAngle = (startingAngle + 180)%360;
        Vector2 reverseDirection = new Vector2(Mathf.Cos(reverseAngle * Mathf.Deg2Rad), Mathf.Sin(reverseAngle * Mathf.Deg2Rad));
        rotationCenter = rb.position + radius * reverseDirection.normalized;

        currentAngle = startingAngle;
        deltaAngle = 0;

        deltaRad = 0;
        currentRad = currentAngle * Mathf.Deg2Rad;

        if(data.clockwise)
            rotationDir = -1;
        else   
            rotationDir = 1;
            
        numRevolutions = 0;
    }

    public void SetupSin(SinData data = null){
        Reset();
        startPosition = rb.position;
        moveDirection = new Vector2(0,-1);
        amplitude = data.amplitude;
        frequency = data.frequency;
        halfCycleDist = (1/frequency) * Mathf.PI;
    }

    public void UpdatePiece(){
        pieceTime += Time.deltaTime;

        pieceDist += Vector2.Distance(rb.position, lastPosition);
        pieceDistX += Mathf.Abs(rb.position.x - lastPosition.x);
        pieceDistY += Mathf.Abs(rb.position.y - lastPosition.y);

        lastPosition = rb.position;
    }

    public Vector2 StopMovement(){
        newPosition = rb.position;
        UpdatePiece();
        return newPosition;
    }
    public Vector2 LineMovement(){
        newPosition = rb.position + lineDirection * tmp.movementSpeed * Time.deltaTime;
        // Debug.Log(lineDirection + " " + tmp.movementSpeed);
        UpdatePiece();
        return newPosition;
    }
    public Vector2 CurveMovement(){
        float x = rotationCenter.x + Mathf.Cos(currentRad) * radius;
        float y = rotationCenter.y + Mathf.Sin(currentRad) * radius;
        newPosition = new Vector2(x,y);

        //Debug.Log(x + ", " + y + " u turn currently" + currentAngle + " radsss " + (currentAngle * Mathf.Deg2Rad) + " delta " + deltaAngle + " rotation center " + rotationCenter);

        currentRad += rotationDir * (tmp.movementSpeed * Time.deltaTime / radius);
        deltaRad += rotationDir * (tmp.movementSpeed * Time.deltaTime / radius);
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

    public Vector2 TestCurveMovement(){
        float x = rotationCenter.x + Mathf.Cos(currentRad) * radius;
        float y = rotationCenter.y + Mathf.Sin(currentRad) * radius;
        newPosition = new Vector2(x,y);

        //Debug.Log(x + ", " + y + " u turn currently" + currentAngle + " radsss " + (currentAngle * Mathf.Deg2Rad) + " delta " + deltaAngle + " rotation center " + rotationCenter);

        currentRad += (tmp.movementSpeed * Time.deltaTime / radius);
        deltaRad += (tmp.movementSpeed * Time.deltaTime / radius);

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

    //Oscillates along X axis, moving in Y axis
    public Vector2 SinXMovement(){
        float distanceTravelled = Mathf.Abs(startPosition.y - rb.position.y);

        newPosition = rb.position + moveDirection * tmp.movementSpeed * Time.deltaTime;
        newPosition.x  = amplitude * Mathf.Sin(distanceTravelled * frequency);

        UpdatePiece();
        
        numHalfCycles = (int)(pieceDistY/halfCycleDist);

        return newPosition;
    }

    public Vector2 ClampSinX(){
        newPosition.x  = startPosition.x;
        return newPosition;
    }

    //Oscillates along Y axis, moving in X axis
    public Vector2 SinYMovement(){
        float distanceTravelled = Mathf.Abs(startPosition.y - rb.position.y);

        newPosition = rb.position + moveDirection * tmp.movementSpeed * Time.deltaTime;
        newPosition.y  = amplitude * Mathf.Sin(distanceTravelled * frequency);

        numHalfCycles = (int)(pieceDistX/halfCycleDist);
        
        // Vector2 tempPos = rb.position;
        // tempPos.y = amplitude * Mathf.Sin(distanceTravelled * frequency);
        // float dist = Vector2.Distance(tempPos, rb.position);
        // if(dist >= tmp.movementSpeed){
        //     Debug.Log("sin speed higher than movement speed");
        //     newPosition = tempPos;
        // }
        // else{
        //     float speed = Mathf.Pow(tmp.movementSpeed, 2) - Mathf.Pow(dist, 2);
        //     newPosition = tempPos + moveDirection * Mathf.Sqrt(speed);
        // }

        UpdatePiece();
        return newPosition;
    }
}
