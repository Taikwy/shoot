using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryGun : Gun
{
    [Header("overheating Stats")]
    public float maxHeat;
    [HideInInspector] public float currentHeat;
    public float overheatAmount;
    public float overheatedPause;
    [HideInInspector] public bool overheated;
    public float cooldownRate;
    public float cooldownPause;
    public float cooldownAmount;

    public void Update(){
        Cooldown();
    }

    public override void Cooldown(){
        if(overheated){
            if(timeSinceShot >= overheatedPause){
                isRecharging = true;
                overheated = false;
            }
        }
        else
            isRecharging = timeSinceShot > cooldownPause;


        timeSinceShot += Time.deltaTime;
        if(isRecharging && currentHeat > 0){
            currentHeat -= cooldownRate*Time.deltaTime;
            if(currentHeat < 0)
                currentHeat = 0;
        }            
    }

    public override void Shoot()
    {
        timeSinceShot = 0;
        currentHeat += overheatAmount;
        if(currentHeat >= maxHeat){
            overheated = true;
            isRecharging = false;
        }
    }
}
