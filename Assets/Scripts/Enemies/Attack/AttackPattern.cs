using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern : MonoBehaviour
{
    public bool canAttack = false;
    // public bool counterAttack = false;
    public enum DAMAGEDTYPE{
        NONE,
        SEQUENCE,
        COUNTER
    }
    public DAMAGEDTYPE damagedType;
    [HideInInspector] public bool isAttacking, damaged, counterAttacking, exiting = false;

    public List<AttackSequence> mainSequences = new List<AttackSequence>();
    public AttackSequence damageSequence, exitSequence;
    // [HideInInspector] public AttackSequence mainSequence;
    protected AttackSequence currentSequence;
    protected List<AttackSequence> currentSequences = new List<AttackSequence>();
    protected List<AttackSequence> tempSequences = new List<AttackSequence>();


    public virtual void Setup(){
        // Debug.Log("setting attack pattern");
        canAttack = true;
        damaged = counterAttacking = exiting = false;
        foreach(AttackSequence mainSeq in mainSequences){
            mainSeq.Setup();
        }
        damageSequence.Setup();
        exitSequence.Setup();
        // SetSequence(mainSequence);
        // SetSequences(mainSequences);
    }

    public virtual void Update(){
        // currentSequence.UpdateSequence();
        // currentSequence.UpdateTimers();

        if(!canAttack)
            return;

        if(counterAttacking){                                               //if damage type is to counterattack, does one sequence and stops once finished
            if(damageSequence.SingleUpdate()){
                counterAttacking = false;
                Debug.Log("compelted single update " + counterAttacking);
            }
        }
        if(exiting){
            exitSequence.UpdateSequence();
            exitSequence.UpdateTimers();
        }
        else if(damaged){
            damageSequence.UpdateSequence();
        }
        else{
            foreach(AttackSequence mainSeq in mainSequences){
                mainSeq.UpdateSequence();
                mainSeq.UpdateTimers();
            }
        }
        damageSequence.UpdateTimers();
    }

    public virtual void TakeDamage(){
        Debug.Log("took damage");
        switch(damagedType){
            case DAMAGEDTYPE.SEQUENCE:  
                damaged = true;
                break;
            case DAMAGEDTYPE.COUNTER:
                counterAttacking = true;
                break;
        }
    }

    public virtual void SetSequence(AttackSequence newSequence){
        currentSequence = newSequence;
    }

    public virtual void SetSequences(List<AttackSequence> newSequences){
        currentSequences = newSequences;
    }
}