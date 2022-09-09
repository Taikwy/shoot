using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAP : AttackPattern
{
    //Shooting Scripts
    SpreadShot spreadShot;

    [Header("Primary Sequence")]
    public SpreadShot.Data primary_spread;

    public void Setup()
    {
        spreadShot = gameObject.GetComponent<SpreadShot>();

        PoolManager.Instance.CreatePool(primary_spread.bulletPrefab, 100, "bullet");

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
                primary_spread.timer += Time.deltaTime;
                PrimarySequence();
                break;
        }
    }

    //basic bullets forward
    protected override void PrimarySequence(){
        if(primary_spread.timer >= primary_spread.shootRate){
            spreadShot.ShootSpread(primary_spread);
            primary_spread.timer = 0;
        }
    }
}
