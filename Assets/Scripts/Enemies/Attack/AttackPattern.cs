using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern : MonoBehaviour
{
    public Transform firingPoint;
    public Shoot shoot;
    // public bool conditional = false;
    public bool shooting = false;

    public enum AttackSequence{
        Primary,
        Conditional,
        Impaired
    }

    public AttackSequence currentSequence;

    public virtual void Reset(){
        ChangeSequence(AttackSequence.Primary);
    }

    protected virtual void Update(){
        // if(conditional){
        //     ChangeSequence(AttackSequence.Conditional);
        //     conditional = false;
        // }
        if(!shooting)
            return;
        switch(currentSequence){
            case AttackSequence.Primary:
                PrimarySequence();
                break;
            case AttackSequence.Conditional:
                ConditionalSequence();
                break;
            case AttackSequence.Impaired:
                ImpairedSequence();
                break;
        }
    }

    public virtual void ChangeSequence(AttackSequence newSequence){
        currentSequence = newSequence;
    }
    
    //basic bullets forward
    protected virtual void PrimarySequence(){
    }

    //Getting hit will spray 8 bullets out
    protected virtual void ConditionalSequence(){
    }

    //stops attacking
    protected virtual void ImpairedSequence(){
    }
}
