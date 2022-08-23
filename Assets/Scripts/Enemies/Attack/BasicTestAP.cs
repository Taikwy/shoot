using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTestAP : AttackPattern
{
    public BasicTestMP movementPattern;
    GameObject basicShotPrefab;

    [Header("Shooting Stuff")]
    protected float shootTimer = 0;
    protected float basicShootRate = 1;
    protected float shootRate = 1;

    public void Setup(GameObject prefab)
    {
        basicShotPrefab = prefab;

        PoolManager.Instance.CreatePool(basicShotPrefab, 100);
        ChangeSequence(AttackSequence.Primary);
    }

    // Update is called once per frame
    protected override void Update()
    {
        shootTimer += Time.deltaTime;
        base.Update();
    }

    public override void ChangeSequence(AttackSequence newSequence){
        // Debug.Log(currentSequence + " " + newSequence);
        switch(newSequence){
            case AttackSequence.Primary:
                break;
            case AttackSequence.Conditional:
                break;
            case AttackSequence.Impaired:
                break;
        }
        base.ChangeSequence(newSequence);
    }

    //basic bullets forward
    protected override void PrimarySequence(){
        if(shootTimer >= basicShootRate){
            shoot.ShootForward(basicShotPrefab);
            // testShoot.ShootTriple(basicShotPrefab);
            // testShoot.ShootTowards(basicShotPrefab);
            shootTimer = 0;
        }
        
    }

    //Getting hit will spray 8 bullets out
    protected override void ConditionalSequence(){
        shoot.ShootCircle(basicShotPrefab, 8, true);
        ChangeSequence(AttackSequence.Primary);
    }

    //stops attacking
    protected override void ImpairedSequence(){
    }
}
