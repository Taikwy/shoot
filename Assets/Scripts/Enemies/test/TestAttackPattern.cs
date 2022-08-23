using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttackPattern : MonoBehaviour
{
    [SerializeField]
    // private Rigidbody2D rb;
    public Transform firingPoint;

    enum AttackSequence{
        Primary,
        Conditional,
        Impaired
    }

    AttackSequence currentSequence;

    public TestShoot testShoot;
    public GameObject basicPrefab;

    [Header("Shooting Stuff")]
    public float shootTimer = 0;
    public float basicShootRate = 1;
    public float shootRate = 1;

    // Start is called before the first frame update
    void Start()
    {
        PoolManager.Instance.CreatePool(basicPrefab, 100);
        ChangeSequence(AttackSequence.Primary);
    }

    void Update(){

        shootTimer += Time.deltaTime;

        switch(currentSequence){
            case AttackSequence.Primary:
                PrimarySequence();
                break;
            case AttackSequence.Conditional:
                ConditionalSequence();
                break;
            case AttackSequence.Impaired:
                ImpairedSequence();
                break;
        }
    }

    void ChangeSequence(AttackSequence newSequence){
        switch(newSequence){
            case AttackSequence.Primary:
                break;
            case AttackSequence.Conditional:
                break;
            case AttackSequence.Impaired:
                break;
        }
        
        currentSequence = newSequence;
    }
    
    //basic bullets forward
    void PrimarySequence(){
        if(shootTimer >= basicShootRate){
            testShoot.ShootForward(basicPrefab);
            // testShoot.ShootTriple(basicPrefab);
            // testShoot.ShootTowards(basicPrefab);
            shootTimer = 0;
        }
        
    }

    //Getting hit will spray 8 bullets out
    void ConditionalSequence(){
        testShoot.ShootCircle(basicPrefab, 8, true);
        ChangeSequence(AttackSequence.Primary);
    }

    //stops attacking
    void ImpairedSequence(){
    }
}
