using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAttackPattern : AttackPattern
{
    //Shooting Scripts
    StraightShot straightShot;
    TowardShot towardShot;
    SpreadShot spreadShot;
    CircleShot circleShot;

    [Header("Primary Sequence")]
    public StraightShot.Data primary_straight;
    public TowardShot.Data primary_toward;
    public SpreadShot.Data primary_spread;
    public CircleShot.Data primary_circle;
    [Header("Conditional Sequence")]
    public CircleShot.Data conditional_circle;
    [Header("Impaired Sequence")]
    public StraightShot.Data impared_straight;                  //Doesnt shoot

    public void Setup()
    {
        Debug.Log("setting up A pattern");
        straightShot = gameObject.GetComponent<StraightShot>();
        towardShot = gameObject.GetComponent<TowardShot>();
        spreadShot = gameObject.GetComponent<SpreadShot>();
        circleShot = gameObject.GetComponent<CircleShot>();

        PoolManager.Instance.CreatePool(primary_straight.bulletPrefab, 100, "bullet");
        PoolManager.Instance.CreatePool(primary_toward.bulletPrefab, 100, "bullet");
        PoolManager.Instance.CreatePool(primary_spread.bulletPrefab, 100, "bullet");
        PoolManager.Instance.CreatePool(primary_circle.bulletPrefab, 100, "bullet");
        PoolManager.Instance.CreatePool(conditional_circle.bulletPrefab, 100, "bullet");
        PoolManager.Instance.CreatePool(impared_straight.bulletPrefab, 100, "bullet");

        canAttack = true;
        ChangeSequence(AttackSequence.Primary);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(!canAttack)
            return;
        switch(currentSequence){
            case AttackSequence.Primary:
                primary_straight.timer += Time.deltaTime;
                primary_toward.timer += Time.deltaTime;
                primary_spread.timer += Time.deltaTime;
                primary_circle.timer += Time.deltaTime;
                PrimarySequence();
                break;
            case AttackSequence.Conditional:
                // conditional_circle.timer += Time.deltaTime;
                ConditionalSequence();
                break;
            case AttackSequence.Impaired:
                impared_straight.timer += Time.deltaTime;
                ImpairedSequence();
                break;
        }
    }

    public override void ChangeSequence(AttackSequence newSequence){
        base.ChangeSequence(newSequence);
        switch(newSequence){
            case AttackSequence.Primary:
                // straightShot.SetupType(primary_straight);
                // towardShot.SetupType(primary_toward);
                // spreadShot.SetupType(primary_spread);
                // circleShot.SetupType(primary_circle);
                break;
            case AttackSequence.Conditional:
                // circleShot.SetupType(primary_circle);
                ConditionalSequence();
                break;
            case AttackSequence.Impaired:
                // straightShot.SetupType(primary_straight);
                break;
        }
    }

    //basic bullets forward
    protected override void PrimarySequence(){
        if(primary_straight.timer >= primary_straight.shootRate){
            straightShot.ShootStraight(primary_straight);
            primary_straight.timer = 0;
        }
        if(primary_toward.timer >= primary_toward.shootRate){
            // towardShot.ShootTowards(primary_toward);
            // towardShot.ShootTowardsPlayer(primary_toward);
            primary_toward.timer = 0;
        }
        if(primary_spread.timer >= primary_spread.shootRate){
            // spreadShot.ShootSpread(primary_spread);
            primary_spread.timer = 0;
        }
        if(primary_circle.timer >= primary_circle.shootRate){
            // circleShot.ShootCircle(primary_circle);
            primary_circle.timer = 0;
        }
    }

    //Getting hit will spray 8 bullets out
    protected override void ConditionalSequence(){
        // if(conditional_circle.timer >= conditional_circle.shootRate){
        //     circleShot.ShootCircle(conditional_circle);
        //     conditional_circle.timer = 0;
        // }
        circleShot.ShootCircle(conditional_circle);
        // ChangeSequence(AttackSequence.Primary);
    }

    //stops attacking
    protected override void ImpairedSequence(){
        if(impared_straight.timer >= impared_straight.shootRate){
            straightShot.ShootStraight(impared_straight);
            impared_straight.timer = 0;
        }
    }
}
