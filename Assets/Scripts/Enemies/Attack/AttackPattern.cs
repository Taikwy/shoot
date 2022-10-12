using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern : MonoBehaviour
{
    public bool canAttack = false;
    public bool counterAttack = false;

    public AttackSequence mainSequence, damageSequence, exitSequence;
    protected AttackSequence currentSequence;

    public virtual void Setup(){
        Debug.Log("setting attack pattern");
        canAttack = true;
        if(mainSequence)
            mainSequence.Setup();
        SetSequence(mainSequence);
    }

    public virtual void Update(){
        currentSequence.UpdateSequence(canAttack);
    }

    public virtual void SetSequence(AttackSequence newSequence){
        currentSequence = newSequence;
    }
}