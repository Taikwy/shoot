using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovementPattern : MovementPattern
{
    public AttackPattern attackPattern;
    bool mirrored;
    string currentPiece;


    public void Setup(bool m = false){
        Debug.Log("setting pattern");
        currentSequence = sequences[currentSequenceIndex];
        currentSequence.Setup(gameObject);

        mirrored = m;

        base.Setup();
    }

    public override void ChangeSequence(MovementSeq newSequence){
        base.ChangeSequence(newSequence);
    }
}
