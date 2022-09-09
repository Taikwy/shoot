using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeAP : AttackPattern
{
    //Shooting Scripts
    TowardShot towardShot;
    CircleShot circleShot;

    [Header("Primary Sequence")]
    public TowardShot.Data primary_toward;
    [Header("Conditional Sequence")]
    public CircleShot.Data conditional_circle;

    public void Setup()
    {
        towardShot = gameObject.GetComponent<TowardShot>();
        circleShot = gameObject.GetComponent<CircleShot>();

        PoolManager.Instance.CreatePool(primary_toward.bulletPrefab, 100, "bullet");
        PoolManager.Instance.CreatePool(conditional_circle.bulletPrefab, 100, "bullet");

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
                primary_toward.timer += Time.deltaTime;
                PrimarySequence();
                break;
            case AttackSequence.Conditional:
                ConditionalSequence();
                break;
        }
    }

    public override void ChangeSequence(AttackSequence newSequence){
        base.ChangeSequence(newSequence);
        switch(newSequence){
            case AttackSequence.Conditional:
                ConditionalSequence();
                break;
        }
    }

    //basic bullets forward
    protected override void PrimarySequence(){
        if(primary_toward.timer >= primary_toward.shootRate){
            towardShot.ShootTowardsPlayer(primary_toward);
            primary_toward.timer = 0;
        }
    }

    //Getting hit will spray 8 bullets out
    protected override void ConditionalSequence(){
        circleShot.ShootCircle(conditional_circle);
    }
}
