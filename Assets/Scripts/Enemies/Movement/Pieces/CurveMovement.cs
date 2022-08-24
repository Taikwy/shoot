using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : MovementPiece
{
    [Header("Curve Info")]
    float radius, startingAngle;
    bool clockwise, mirrored;
    Vector2 rotationCenter;
    float currentAngle, currentRad;                 //Actual angle based off center
    int rotationDir, numRevolutions;
    [HideInInspector]
    public float deltaAngle, deltaRad;            //How far its curved

    [System.Serializable]
    public struct CurveData{
        public float radius, startingAngle;
        public bool clockwise;
        public float distanceToCurve;                                //how far to turn before swapping piece
    }

    public void Setup(CurveData data, bool m = false){
        SetupPiece();

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


            float reverseAngle = (startingAngle + 180)%360;
            Vector2 reverseDirection = new Vector2(Mathf.Cos(reverseAngle * Mathf.Deg2Rad), Mathf.Sin(reverseAngle * Mathf.Deg2Rad));
            rotationCenter = (Vector2)objectTransform.position + radius * reverseDirection.normalized;
            Debug.Log("mirrored "+ rotationCenter + " " + reverseDirection + " " + reverseAngle + " " + startingAngle);
        }
        else{
            float reverseAngle = (startingAngle + 180)%360;
            Vector2 reverseDirection = new Vector2(Mathf.Cos(reverseAngle * Mathf.Deg2Rad), Mathf.Sin(reverseAngle * Mathf.Deg2Rad));
            rotationCenter = (Vector2)objectTransform.position + radius * reverseDirection.normalized;
        }
        
        currentAngle = startingAngle;
        currentRad = currentAngle * Mathf.Deg2Rad;
        deltaAngle = deltaRad = 0;
    }

    public Vector2 Movement(float speed){
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
            objectTransform.up = Vector2.Perpendicular(newPosition - rotationCenter);
        else
            objectTransform.up = Vector2.Perpendicular(rotationCenter - newPosition);
    }
}
