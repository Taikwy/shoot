using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAP : AttackPattern
{
    //Shooting Scripts
    StraightShot straightShot;

    [Header("Primary Sequence")]
    public StraightShot.Data primary_straight;

    public void Setup()
    {
        straightShot = gameObject.GetComponent<StraightShot>();

        PoolManager.Instance.CreatePool(primary_straight.bulletPrefab, 100, "bullet");

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
                PrimarySequence();
                break;
        }
    }

    //basic bullets forward
    protected override void PrimarySequence(){
        if(primary_straight.timer >= primary_straight.shootRate){
            straightShot.ShootStraight(primary_straight);
            primary_straight.timer = 0;
        }
    }
}
