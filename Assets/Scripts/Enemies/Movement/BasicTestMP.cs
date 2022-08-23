using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTestMP : MovementPattern
{
    // public Collider2D collider;
    public BasicTestAP attackPattern;
    public SpawnLine spawnSequence;
    public LineStop lineStop;
    public CurveLine curveLine;    
    SinData sinData;
    bool mirrored;

    // Start is called before the first frame update
    void Start(){}

    public void Setup(LineStopData lineStopData, CurveLineData uTurnData, SinData sin, bool m = false){
        movementPieces = gameObject.GetComponent<MovementPieces>();
        
        movementPieces.ResetPiece();
        
        curveLine.curveLineData = uTurnData;
        lineStop.lineStopData = lineStopData;
        sinData = sin;
        mirrored = m;

        maxMovementSpeed = 5f;
        movementSpeed = maxMovementSpeed;
        acceleration = 4f;
        // currentSequence = MovementSequence.Spawn;

        // spawnSequence.Setup();
        // SpawnSequence();

        ChangeSequence(MovementSequence.Enter);
    }

    protected override void ChangeSequence(MovementSequence newSequence){
        switch(newSequence){
            case MovementSequence.Enter:
                // collider.enabled = false;
                attackPattern.shooting = false;
                lineStop.Setup();
                EnterSequence();
                break;
            case MovementSequence.Main:
                // collider.enabled = true;
                attackPattern.shooting = true;
                movementPieces.SetupSin(sinData);
                MainSequence();
                break;
            case MovementSequence.Exit:
                // gameObject.GetComponent<EnemyScript>().exiting = true;
                attackPattern.shooting = false;
                SetSpeedAndAccel(60f, 2f, 15f);
                curveLine.Setup("curve");
                ExitSequence();
                break;
        }
        base.ChangeSequence(newSequence);
    }

    // //line downwards
    // protected override void SpawnSequence(){
    //     if(spawnSequence.spawnMovementComplete){
    //         attackPattern.shooting = true;
    //         ChangeSequence(MovementSequence.Enter);
    //         return;
    //     }
    //     movePosition = spawnSequence.Movement(movementSpeed);
    // }

    //line and stay
    protected override void EnterSequence(){
        if(lineStop.numSwaps >= 1){
            ChangeSequence(MovementSequence.Main);
            return;
        }
        movePosition = lineStop.Movement(movementSpeed);
    }

    //sin downwards
    protected override void MainSequence(){
        if(movementPieces.numHalfCycles >= 2){
            movePosition = movementPieces.ClampSinX();
            ChangeSequence(MovementSequence.Exit);
            return;
        }
        movePosition = movementPieces.SinXMovement(movementSpeed);
    }

    //curves and goes away
    protected override void ExitSequence(){
        movePosition = curveLine.Movement(movementSpeed);
        if(curveLine.currentPiece == "line")
            SetSpeedAndAccel(900f, movementSpeed, 15f);

    }
}
