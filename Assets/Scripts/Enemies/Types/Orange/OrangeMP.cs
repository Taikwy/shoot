using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMP : MovementPattern
{
    public AttackPattern attackPattern;
    bool mirrored;
    string currentPiece;

    //Movement Scripts
    LineMovement lineMovement;
    StopMovement stopMovement;

    [Header("Enter Sequence")]
    public LineMovement.LineData enter_line;
    public float enterMaxSpeed, enterSpeed, enterAcceleration;
    [Header("Main Sequence")]
    public StopMovement.StopData main_stop;

    public void Setup(bool m = false){
        lineMovement = gameObject.GetComponent<LineMovement>();
        stopMovement = gameObject.GetComponent<StopMovement>();

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
                stopMovement.Setup(main_stop);
                MainSequence();
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

    //stays in place
    protected override void MainSequence(){
        movePosition = stopMovement.Movement(currentSpeed);
    }
}
