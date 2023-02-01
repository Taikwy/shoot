using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeGun : SpecialGun
{
    [Header("charge gun stats")]
    public int maxChargeLevel;
    public int currentChargeLevel;

    public float lv1ChargeTime, lv2ChargeTime, lv3ChargeTime;
    [HideInInspector] float currentChargeTime;

    public override void Start()
    {
        base.Start();
        currentChargeLevel = 0;
    }

    public override void UpdateShooting(){
        
        if (Input.GetKey(playerData.specialFireKey)){
            if(timeSinceShot < 1/roundsPerSecond)               //If player tries to charge their next shot too quickly, it wont register the charge time
                return;

            Debug.Log("charging");
            currentChargeTime += Time.deltaTime;
            
        }
        if (Input.GetKeyUp(playerData.specialFireKey)){   
            if(currentChargeTime >= lv3ChargeTime){
                currentChargeLevel = 3;
            }
            else if(currentChargeTime >= lv2ChargeTime){
                currentChargeLevel = 2;
            }
            else if(currentChargeTime >= lv1ChargeTime){
                currentChargeLevel = 1;
            }
            else{
                currentChargeLevel = 0;
            }
            if(ammoCost <= currentAmmo && timeSinceShot >= 1/roundsPerSecond){
                Shoot();
            }         
            currentChargeTime = 0;     
            currentChargeLevel = 0;
        }
    }

    public override void Shoot(){            
        Debug.Log("charge time " +  currentChargeTime);

        currentBulletPrefab = bulletPrefabs[currentChargeLevel];

        Vector2 spawnPos = new Vector2(firingPoints[0].position.x, firingPoints[0].position.y);
        GameObject bullet = PoolManager.Instance.ReuseObject(currentBulletPrefab, spawnPos, firingPoints[0].rotation);
        bullet.GetComponent<BulletScript>().SetData(false, firingPoints[0].up);
        
        base.Shoot();
    }
}
