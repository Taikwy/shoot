using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineStop : MonoBehaviour
{
    [Header("Movement Component Info")]
    [SerializeField]
    private Rigidbody2D rb;
    public Transform t;
    public MovementPieces movementPieces;
    Vector2 lastPosition;

    [Header("Movement Type Data")]
    //Movement Info ================================================================================================
    public LineStopData lineStopData;
    LineData lineData;
    string currentPiece;
    

    //Piece conditions ------------------------------
    string lineConditionType;                           //distance travelled || time
    float lineCondition, stopCondition;                                //Can only be time
    public int numSwaps;

    public void Setup(string piece = "line"){
        // lastPosition = rb.position;
        lastPosition = gameObject.transform.position;

        lineData = lineStopData.lineData;
        lineConditionType = lineStopData.lineConditionType;
        lineCondition = lineStopData.lineCondition;
        stopCondition = lineStopData.stopCondition;
        numSwaps = 0;

        currentPiece = piece;
        if(currentPiece == "line")
            movementPieces.SetupLine(lineData);
        else   
            movementPieces.SetupStop();

    }

    public Vector2 Movement(float speed){
        //Checks if current type behavior is finished
        switch(currentPiece){
            case "line":
                movementPieces.LineMovement(speed);
                if(movementPieces.pieceDist >= lineCondition){
                    currentPiece = "stop";
                    movementPieces.SetupStop();
                    numSwaps++;
                }
                break;
            case "stop":
                movementPieces.StopMovement();
                if(movementPieces.pieceTime >= stopCondition){
                    currentPiece = "line";
                    movementPieces.SetupLine(lineData);
                    numSwaps++;
                }
                break;
        }

        lastPosition = movementPieces.lastPosition;
        return movementPieces.newPosition;
    }
}
