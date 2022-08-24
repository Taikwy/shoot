using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTestScript : MinorEnemyScript
{
    
    [Header("Behavior Scripts")]
    public NewMovementPattern movementPattern;
    public NewAttackPattern attackPattern;
    public GameObject basicShotPrefab;

    void Start()
    {
        Debug.Log("object starting");
        health = 5;
        exiting = false;
        movementPattern.Setup();
        attackPattern.Setup();
    }

    void Update(){
        // if(movementPattern.currentSequence == MovementPattern.MovementSequence.Exit)
        //     attackPattern.shooting = false;
    }

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        Debug.Log("object reusing");
        health = 5;
        exiting = false;
        movementPattern.Setup();
        attackPattern.Setup();
    }

    //Reset behavior stuff but keep stats cuz its jsut respawning
    public override void OnObjectRespawn(){
        Debug.Log("on object respawning");
        exiting = false;
        movementPattern.Setup();
        attackPattern.Setup();
    }

    public override void TakeDamage(BulletScript bulletScript){
        base.TakeDamage(bulletScript);
        if(health <= 2){
            attackPattern.ChangeSequence(AttackPattern.AttackSequence.Conditional);
            attackPattern.ChangeSequence(AttackPattern.AttackSequence.Impaired);
        }
        else{
            attackPattern.ChangeSequence(AttackPattern.AttackSequence.Conditional);
            attackPattern.ChangeSequence(AttackPattern.AttackSequence.Primary);
        }
        if(health <= 1){
            movementPattern.ChangeSequence(MovementPattern.MovementSequence.Exit);
            attackPattern.canAttack = false;
        }
    }

    public override void DropScrap(string bulletType){
        GameObject scrap = Instantiate(scrapPrefab, transform.position, Quaternion.identity);
        switch(bulletType){
            case "default":
                scrap.GetComponent<ScrapScript>().SetData(defaultScrap);
                break;
            case "skill":
                scrap.GetComponent<ScrapScript>().SetData(skillScrap);
                break;
            case "both":
                scrap.GetComponent<ScrapScript>().SetData(bothScrap);
                break;
        }
    }
}
