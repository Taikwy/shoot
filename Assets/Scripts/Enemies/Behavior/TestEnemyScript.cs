using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyScript : MinorEnemyScript
{
    [Header("Behavior Pattern Scripts")]
    public TestMovementPattern movementPattern;
    public NewAttackPattern attackPattern;
    void Start()
    {
        Debug.Log("object starting");
        currentHealth = maxHealth;
        movementPattern.Setup();
        // attackPattern.Setup();
    }

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        Debug.Log("enemy reusing");
        currentHealth = maxHealth;
        movementPattern.Setup();
        // attackPattern.Setup();
    }

    //Reset behavior stuff but keep stats cuz its jsut respawning
    public override void OnObjectRespawn(){
        Debug.Log("on object respawning");
        movementPattern.Setup();
        // attackPattern.Setup();
    }

    public override void TakeDamage(BulletScript bulletScript){
        base.TakeDamage(bulletScript);
    }
}
