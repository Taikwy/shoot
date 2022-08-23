using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTurn : MonoBehaviour
{
    [Header("Movement Component Info")]
    [SerializeField]
    private Rigidbody2D rb;
    public TestMovementPiece testMovementPiece;


    [Header("Movement Type Data")]
    //Movement Info ================================================================================================
    Vector2 lastPosition;

    [SerializeField]    
    LineStopData lineStopData;
    public string currentPiece;
    
    LineData lineData;
    CurveData curveData;

    //Piece conditions ------------------------------
    string lineConditionType;                           //distance travelled || time
    float lineCondition;
    // float curveCondition;                                //180 degrees

    public void Setup(string piece = "line"){
        lastPosition = rb.position;

        lineData = lineStopData.lineData;
        lineConditionType = lineStopData.lineConditionType;
        lineCondition = lineStopData.lineCondition;

        currentPiece = piece;
        testMovementPiece.SetupCurve(curveData);
    }

    public Vector2 Movement(){
        //Checks if current type behavior is finished
        switch(currentPiece){
            case "curve":
                testMovementPiece.StopMovement();
                if(testMovementPiece.deltaAngle >= 180){
                    currentPiece = "line";
                    testMovementPiece.SetupLine(lineData);
                }
                break;
            case "line":
                testMovementPiece.LineMovement();
                break;
        }

        lastPosition = testMovementPiece.lastPosition;
        return testMovementPiece.newPosition;
    }
}
