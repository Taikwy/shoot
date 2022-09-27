using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementPattern : MovementPattern
{
    public AttackPattern attackPattern;
    bool mirrored;
    string currentPiece;

    //Movement Scripts
    LineMovement lineMovement;
    StopMovement stopMovement;
    SinMovement sinMovement;
    CurveMovement curveMovement;

    [Header("Enter Sequence")]
    public LineMovement.LineData enter_line;
    public StopMovement.StopData enter_stop;
    public float enterMaxSpeed, enterSpeed, enterAcceleration;
    [Header("Main Sequence")]
    public SinMovement.SinData main_sin;
    public float mainMaxSpeed, mainSpeed, mainAcceleration;
    [Header("Exit Sequence")]
    public CurveMovement.CurveData exit_curve;
    public LineMovement.LineData exit_line;
    public float exitMaxSpeed, exitSpeed, exitAcceleration;

    public void Setup(bool m = false){
        Debug.Log("setting up m pattern");
        lineMovement = gameObject.GetComponent<LineMovement>();
        stopMovement = gameObject.GetComponent<StopMovement>();
        sinMovement = gameObject.GetComponent<SinMovement>();
        curveMovement = gameObject.GetComponent<CurveMovement>();

        mirrored = m;

        base.Setup();
        ChangeSequence(MovementSeq.Enter);
    }

    public override void ChangeSequence(MovementSeq newSequence){
        base.ChangeSequence(newSequence);
        switch(newSequence){
            case MovementSeq.Enter:
                lineMovement.Setup(enter_line);
                SetSpeedAndAccel(enterMaxSpeed, enterSpeed, enterAcceleration);
                EnterSequence();
                break;
            case MovementSeq.Main:
                sinMovement.Setup(main_sin);
                SetSpeedAndAccel(mainMaxSpeed, mainSpeed, mainAcceleration);
                MainSequence();
                break;
            case MovementSeq.Exit:
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
            ChangeSequence(MovementSeq.Main);
            return;
        }
        movePosition = lineMovement.Movement(currentSpeed);
    }

    //sin downwards
    protected override void MainSequence(){
        if(sinMovement.numHalfCycles >= main_sin.numHalfCycles){
            movePosition = sinMovement.ClampSinX();
            ChangeSequence(MovementSeq.Exit);
            return;
        }
        movePosition = sinMovement.MovementX(currentSpeed);
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
                    SetSpeedAndAccel(900f, currentSpeed, 15f);
                }
                break;
            case "line":
                movePosition = lineMovement.Movement(currentSpeed);
                break;
        }            
    }
}
