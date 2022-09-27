using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenScript : MinorEnemyScript
{
    [Header("Behavior Pattern Scripts")]
    public GreenMP movementPattern;
    public GreenAP attackPattern;
    void Start()
    {
        Debug.Log("object starting");
        currentHealth = maxHealth;
        movementPattern.Setup();
        attackPattern.Setup();
    }

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        Debug.Log("enemy reusing");
        currentHealth = maxHealth;
        movementPattern.Setup();
        attackPattern.Setup();
    }

    //Reset behavior stuff but keep stats cuz its jsut respawning
    public override void OnObjectRespawn(){
        Debug.Log("on object respawning");
        movementPattern.Setup();
        attackPattern.Setup();
    }

    public override void TakeDamage(BulletScript bulletScript){
        base.TakeDamage(bulletScript);

        if(movementPattern.currentSeq != MovementPattern.MovementSeq.Exit){
            if(currentHealth <= 1){
                movementPattern.ChangeSequence(MovementPattern.MovementSeq.Exit);
            }
            else if(currentHealth <= 3){
                attackPattern.ChangeSequence(AttackPattern.AttackSequence.Conditional);
                attackPattern.ChangeSequence(AttackPattern.AttackSequence.Impaired);
            }
            else{
                attackPattern.ChangeSequence(AttackPattern.AttackSequence.Conditional);
                attackPattern.ChangeSequence(AttackPattern.AttackSequence.Primary);
            }

        }
    }
}
