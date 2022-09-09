using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkMP : MovementPattern
{
    public AttackPattern attackPattern;
    bool mirrored;
    string currentPiece;

    //Movement Scripts
    LineMovement lineMovement;

    [Header("Enter Sequence")]
    public LineMovement.LineData enter_line;
    public float enterMaxSpeed, enterSpeed, enterAcceleration;

    public void Setup(bool m = false){
        lineMovement = gameObject.GetComponent<LineMovement>();
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
        }
    }
    
    //line downwards
    protected override void EnterSequence(){
        movePosition = lineMovement.Movement(currentSpeed);
    }
}
