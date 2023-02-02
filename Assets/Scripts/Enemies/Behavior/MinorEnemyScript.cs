using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorEnemyScript : EnemyScript
{
    [Header("Behavior Pattern Scripts")]
    protected MovementPattern movementPattern;
    protected AttackPattern attackPattern;
    void Start()
    {
        movementPattern = gameObject.GetComponent<MovementPattern>();
        attackPattern = gameObject.GetComponent<AttackPattern>();

        Debug.Log("minor enemy starting " + movementPattern);
        currentHealth = maxHealth;
        movementPattern.Setup();
        attackPattern.Setup();
    }

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        movementPattern = gameObject.GetComponent<MovementPattern>();
        attackPattern = gameObject.GetComponent<AttackPattern>();
        Debug.Log("enemy reusing " + movementPattern);
        currentHealth = maxHealth;
        movementPattern.Setup();
        attackPattern.Setup();
    }

    public override void TakeDamage(BulletScript bulletScript)
    {
        base.TakeDamage(bulletScript);
        attackPattern.TakeDamage();
    }

    //Reset behavior stuff but keep stats cuz its jsut respawning
    //Only major enemies can respawn

    // public override void OnObjectRespawn(){
    //     Debug.Log("on object respawning");
    //     movementPattern.Setup();
    //     // attackPattern.Setup();
    // }
}
