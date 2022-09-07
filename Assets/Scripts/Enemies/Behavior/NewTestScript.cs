using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTestScript : MinorEnemyScript
{
    [Header("Behavior Pattern Scripts")]
    public NewMovementPattern movementPattern;
    public NewAttackPattern attackPattern;
    void Start()
    {
        Debug.Log("object starting");
        currentHealth = maxHealth;
        movementPattern.Setup();
        attackPattern.Setup();

        
        // PoolManager.Instance.CreatePool(newScrapPrefab, 50, "scrap");
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

        if(movementPattern.currentSequence != MovementPattern.MovementSequence.Exit){
            if(currentHealth <= 1){
                movementPattern.ChangeSequence(MovementPattern.MovementSequence.Exit);
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
