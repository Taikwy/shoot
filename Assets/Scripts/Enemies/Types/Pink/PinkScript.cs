using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkScript : MinorEnemyScript
{
    [Header("Behavior Pattern Scripts")]
    public PinkMP movementPattern;
    public PinkAP attackPattern;
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
}
