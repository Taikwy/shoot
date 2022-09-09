using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMP : MovementPattern
{
    public AttackPattern attackPattern;
    bool mirrored;
    string currentPiece;

    //Movement Scripts
    LineMovement lineMovement;
    StopMovement stopMovement;
    CurveMovement curveMovement;

    [Header("Enter Sequence")]
    public LineMovement.LineData enter_line;
    public float enterMaxSpeed, enterSpeed, enterAcceleration;
    [Header("Main Sequence")]
    public StopMovement.StopData main_stop;
    [Header("Exit Sequence")]
    public CurveMovement.CurveData exit_curve;
    public LineMovement.LineData exit_line;
    public float exitMaxSpeed, exitSpeed, exitAcceleration;

    public void Setup(bool m = false){
        lineMovement = gameObject.GetComponent<LineMovement>();
        stopMovement = gameObject.GetComponent<StopMovement>();
        curveMovement = gameObject.GetComponent<CurveMovement>();

        mirrored = m;

        base.Setup();
        ChangeSequence(MovementSequence.Enter);
    }

    public override void ChangeSequence(MovementSequence newSequence){
        base.ChangeSequence(newSequence);
        switch(newSequence){
            case MovementSequence.Enter:
                lineMovement.Setup(enter_line);
                SetSpeedAndAccel(enterMaxSpeed, enterSpeed, enterAcceleration);
                EnterSequence();
                break;
            case MovementSequence.Main:
                stopMovement.Setup(main_stop);
                MainSequence();
                break;
            case MovementSequence.Exit:
                attackPattern.canAttack = false;
                currentPiece = "curve";
                curveMovement.Setup(exit_curve);
                SetSpeedAndAccel(exitMaxSpeed, exitSpeed, exitAcceleration);
                ExitSequence();
                break;
        }
    }
    
    //line downwards
    protected override void EnterSequence(){
        if(lineMovement.pieceDist >= enter_line.distanceToTravel){
            ChangeSequence(MovementSequence.Main);
            return;
        }
        movePosition = lineMovement.Movement(currentSpeed);
    }

    //stays in place
    protected override void MainSequence(){
        if(stopMovement.pieceTime >= main_stop.stopTime){
            ChangeSequence(MovementSequence.Exit);
            return;
        }
        movePosition = stopMovement.Movement(currentSpeed);
    }

    //curves and goes away
    protected override void ExitSequence(){
        switch(currentPiece){
            case "curve":
                movePosition = curveMovement.Movement(currentSpeed);
                curveMovement.CurveRotate();
                if(Mathf.Abs(curveMovement.deltaAngle) >= exit_curve.distanceToCurve){
                    currentPiece = "line";
                    lineMovement.Setup(exit_line, mirrored);
                }
                break;
            case "line":
                movePosition = lineMovement.Movement(currentSpeed);
                break;
        }            
    }
}
