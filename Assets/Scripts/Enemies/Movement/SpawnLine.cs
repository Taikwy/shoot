using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLine : MonoBehaviour
{
    [Header("Movement Component Info")]
    [SerializeField]
    private Rigidbody2D rb;
    public Transform t;
    public MovementPieces movementPieces;
    Vector2 lastPosition;

    [Header("Movement Type Data")]
    //Movement Info ================================================================================================
    SpawnLineData spawnLineData;
    LineData lineData;
    string currentPiece;
    

    //Piece conditions ------------------------------
    string lineConditionType;                           //distance travelled || time
    float lineCondition;                                //Can only be time

    float distanceToActivation;
    public int numSwaps;
    public bool spawnMovementComplete;

    public void Setup(float upperYBound = 0){
        lastPosition = gameObject.transform.position;

        lineData = spawnLineData.lineData;
        distanceToActivation = lastPosition.y - (upperYBound + spawnLineData.spawnOffset.y);
        spawnMovementComplete = false;

        movementPieces.SetupLine(lineData);
    }

    public void Setup(Vector2 spawnPosition){
        gameObject.transform.position = spawnPosition;
        lastPosition = gameObject.transform.position;

        lineData = spawnLineData.lineData;
        lineConditionType = spawnLineData.lineConditionType;
        lineCondition = spawnLineData.lineCondition;
        numSwaps = 0;

        movementPieces.SetupLine(lineData);
    }

    public Vector2 Movement(float speed){
        //Checks if current type behavior is finished
        movementPieces.LineMovement(speed);
        if(movementPieces.pieceDist >= distanceToActivation){
            spawnMovementComplete = true;
        }

        lastPosition = movementPieces.lastPosition;
        return movementPieces.newPosition;
    }
}
