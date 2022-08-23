using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveLine : MonoBehaviour
{
    [Header("Movement Component Info")]
    [SerializeField]
    private Rigidbody2D rb;
    public MovementPieces movementPieces;
    Vector2 lastPosition; 

    [Header("Movement Type Data")]
    //Movement Info ================================================================================================
    public CurveLineData curveLineData;
    LineData lineData;
    CurveData curveData;
    public string currentPiece;
    

    //Piece conditions ------------------------------
    string lineConditionType;                           //distance travelled || time
    float lineCondition, curveAngleCondition;                                //how far to turn
    bool mirrored;

    public void Setup(string piece = "curve", bool m = false){
        // rb = gameObject.GetComponent<Rigidbody2D>();
        // movementPieces = gameObject.GetComponent<MovementPieces>();
        lastPosition = rb.position;

        lineData = curveLineData.lineData;
        curveData = curveLineData.curveData;

        lineConditionType = curveLineData.lineConditionType;
        lineCondition = curveLineData.lineCondition;

        curveAngleCondition = curveLineData.curveAngleCondition;
        mirrored = m;

        currentPiece = piece;
        movementPieces.SetupCurve(curveData, mirrored);
    }

    public Vector2 Movement(float speed){
        //Checks if current type behavior is finished
        switch(currentPiece){
            case "curve":
                movementPieces.CurveMovement(speed);
                movementPieces.CurveRotate();
                if(Mathf.Abs(movementPieces.deltaAngle) >= curveAngleCondition){
                    currentPiece = "line";
                    movementPieces.SetupLine(lineData, mirrored);
                }
                break;
            case "line":
                movementPieces.LineMovement(speed);
                break;
        }

        lastPosition = movementPieces.lastPosition;
        return movementPieces.newPosition;
    }
}
