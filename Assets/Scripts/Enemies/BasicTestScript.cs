using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTestScript : MinorEnemyScript
{
    public BasicTestMP movementPattern;
    [Header("Enter Movement Sequence")]
    public LineStopData lineStop;
    [Header("Main Movement Sequence")]
    public SinData sinData;
    [Header("Exit Movement Sequence")]
    public CurveLineData uTurn;
    public BasicTestAP attackPattern;
    public GameObject basicShotPrefab;
    

    void Start()
    {
        // movementPattern.Setup(lineStop, uTurn, sinData);
        // attackPattern.Setup(basicShotPrefab);
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
        movementPattern.Setup(lineStop, uTurn, sinData);
        attackPattern.Setup(basicShotPrefab);
    }

    //Reset behavior stuff but keep stats cuz its jsut respawning
    public override void OnObjectRespawn(){
        Debug.Log("on object respawning");
        exiting = false;
        movementPattern.Setup(lineStop, uTurn, sinData);
        attackPattern.Setup(basicShotPrefab);
    }

    public override void TakeDamage(BulletScript bulletScript){
        base.TakeDamage(bulletScript);
        if(health < 2)
            attackPattern.ChangeSequence(AttackPattern.AttackSequence.Impaired);
        else
            attackPattern.ChangeSequence(AttackPattern.AttackSequence.Conditional);
            // attackPattern.conditional = true;
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
