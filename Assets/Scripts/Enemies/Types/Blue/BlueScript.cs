using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueScript : MinorEnemyScript
{
    [Header("Behavior Pattern Scripts")]
    public BlueMP movementPattern;
    public BlueAP attackPattern;
    void Start()
    {
        currentHealth = maxHealth;
        movementPattern.Setup();
        attackPattern.Setup();
    }

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        currentHealth = maxHealth;
        movementPattern.Setup();
        attackPattern.Setup();
    }

    //Reset behavior stuff but keep stats cuz its jsut respawning
    public override void OnObjectRespawn(){
        movementPattern.Setup();
        attackPattern.Setup();
    }

    public override void TakeDamage(BulletScript bulletScript){
        base.TakeDamage(bulletScript);

        if(movementPattern.currentSequence != MovementPattern.MovementSequence.Exit){
            if(currentHealth <= 2){
                attackPattern.ChangeSequence(AttackPattern.AttackSequence.Impaired);
            }
            else{
                attackPattern.ChangeSequence(AttackPattern.AttackSequence.Primary);
            }
        }
    }
}
