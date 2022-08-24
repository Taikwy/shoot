using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementPattern : MovementPattern
{
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
    [Header("Main Sequence")]
    public SinMovement.SinData main_sin;
    [Header("Exit Sequence")]
    public CurveMovement.CurveData exit_curve;
    public LineMovement.LineData exit_line;

    public void Setup(bool m = false){
        Debug.Log("setting up m pattern");
        lineMovement = gameObject.GetComponent<LineMovement>();
        stopMovement = gameObject.GetComponent<StopMovement>();
        sinMovement = gameObject.GetComponent<SinMovement>();
        curveMovement = gameObject.GetComponent<CurveMovement>();

        mirrored = m;
        maxMovementSpeed = 3f;
        movementSpeed = maxMovementSpeed;
        acceleration = 4f;

        base.Setup();
        ChangeSequence(MovementSequence.Enter);
    }

    public override void ChangeSequence(MovementSequence newSequence){
        base.ChangeSequence(newSequence);
        switch(newSequence){
            case MovementSequence.Enter:
                lineMovement.Setup(enter_line);
                EnterSequence();
                break;
            case MovementSequence.Main:
                sinMovement.Setup(main_sin);
                MainSequence();
                break;
            case MovementSequence.Exit:
                SetSpeedAndAccel(60f, 2f, 15f);
                curveMovement.Setup(exit_curve);
                currentPiece = "curve";
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
        movePosition = lineMovement.Movement(movementSpeed);
    }

    //sin downwards
    protected override void MainSequence(){
        if(sinMovement.numHalfCycles >= main_sin.numHalfCycles){
            movePosition = sinMovement.ClampSinX();
            ChangeSequence(MovementSequence.Exit);
            return;
        }
        movePosition = sinMovement.MovementX(movementSpeed);
    }

    //curves and goes away
    protected override void ExitSequence(){
        
        switch(currentPiece){
            case "curve":
                movePosition = curveMovement.Movement(movementSpeed);
                curveMovement.CurveRotate();
                if(Mathf.Abs(curveMovement.deltaAngle) >= exit_curve.distanceToCurve){
                    currentPiece = "line";
                    lineMovement.Setup(exit_line, mirrored);
                    SetSpeedAndAccel(900f, movementSpeed, 15f);
                }
                break;
            case "line":
                movePosition = lineMovement.Movement(movementSpeed);
                break;
        }            
    }
}
