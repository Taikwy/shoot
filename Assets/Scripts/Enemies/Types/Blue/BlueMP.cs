using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMP : MovementPattern
{
    public AttackPattern attackPattern;
    bool mirrored;
    string currentPiece;

    //Movement Scripts
    SinMovement sinMovement;

    [Header("Enter Sequence")]
    public SinMovement.SinData enter_sin;
    public float enterMaxSpeed, enterSpeed, enterAcceleration;

    public void Setup(bool m = false){
        sinMovement = gameObject.GetComponent<SinMovement>();
        mirrored = m;

        base.Setup();
        ChangeSequence(MovementState.Enter);
    }

    public override void ChangeSequence(MovementState newSequence){
        base.ChangeSequence(newSequence);
        switch(newSequence){
            case MovementState.Enter:
                sinMovement.Setup(enter_sin);
                SetSpeedAndAccel(enterMaxSpeed, enterSpeed, enterAcceleration);
                EnterSequence();
                break;
        }
    }
    
    //line downwards
    protected override void EnterSequence(){
        movePosition = sinMovement.MovementX(currentSpeed);
    }
}
