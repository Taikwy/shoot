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
    }

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        // gameObject.SetActive(false);
        movementPattern = gameObject.GetComponent<MovementPattern>();
        attackPattern = gameObject.GetComponent<AttackPattern>();
        // Debug.Log("enemy reusing " + movementPattern);
        currentHealth = maxHealth;
    }

    public override void SetupPatterns(){
        movementPattern.Setup();
        attackPattern.Setup();
        // gameObject.SetActive(true);
    }

    public override void TakeDamage(BulletScript bulletScript)
    {
        base.TakeDamage(bulletScript);
        attackPattern.TakeDamage();
    }
}
