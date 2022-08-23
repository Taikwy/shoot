using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementPattern : MovementPattern
{
    public BasicTestAP attackPattern;
    public SpawnLine spawnSequence;
    bool mirrored;
    string currentPiece;

    //Movement Scripts
    LineMovement lineScript;
    StopMovement stopScript;
    SinMovement sinScript;
    CurveMovement curveScript;

    [Header("Enter Sequence")]
    public LineMovement.LineData enter_line;
    public StopMovement.StopData enter_stop;
    [Header("Main Sequence")]
    public SinMovement.SinData main_sin;
    [Header("Exit Sequence")]
    public CurveMovement.CurveData exit_curve;
    public LineMovement.LineData exit_line;

    public void Setup(bool m = false){
        lineScript = gameObject.GetComponent<LineMovement>();
        stopScript = gameObject.GetComponent<StopMovement>();
        sinScript = gameObject.GetComponent<SinMovement>();
        curveScript = gameObject.GetComponent<CurveMovement>();

        mirrored = m;
        maxMovementSpeed = 5f;
        movementSpeed = maxMovementSpeed;
        acceleration = 4f;

        ChangeSequence(MovementSequence.Enter);
    }

    protected override void ChangeSequence(MovementSequence newSequence){
        switch(newSequence){
            case MovementSequence.Enter:
                attackPattern.shooting = false;
                lineScript.Setup(enter_line);
                EnterSequence();
                break;
            case MovementSequence.Main:
                attackPattern.shooting = true;
                sinScript.Setup(main_sin);
                MainSequence();
                break;
            case MovementSequence.Exit:
                attackPattern.shooting = false;
                SetSpeedAndAccel(60f, 2f, 15f);
                curveScript.Setup(exit_curve);
                currentPiece = "curve";
                ExitSequence();
                break;
        }
        base.ChangeSequence(newSequence);
    }
    
    //line downwards
    protected override void EnterSequence(){
        if(lineScript.pieceDist >= enter_line.lineCondition){
            ChangeSequence(MovementSequence.Main);
            return;
        }
        movePosition = lineScript.Movement(movementSpeed);
    }

    //sin downwards
    protected override void MainSequence(){
        if(sinScript.numHalfCycles >= main_sin.numHalfCycles){
            movePosition = sinScript.ClampSinX();
            ChangeSequence(MovementSequence.Exit);
            return;
        }
        movePosition = sinScript.MovementX(movementSpeed);
    }

    //curves and goes away
    protected override void ExitSequence(){
        
        switch(currentPiece){
            case "curve":
                movePosition = curveScript.Movement(movementSpeed);
                curveScript.CurveRotate();
                if(Mathf.Abs(curveScript.deltaAngle) >= exit_curve.curveAngleCondition){
                    currentPiece = "line";
                    lineScript.Setup(exit_line, mirrored);
                    SetSpeedAndAccel(900f, movementSpeed, 15f);
                }
                break;
            case "line":
                movePosition = lineScript.Movement(movementSpeed);
                break;
        }            
    }
}
