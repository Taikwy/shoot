using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    [Header("script refs")]
    public PlayerMovement playerMovement;
    public PlayerController playerController;
    public PlayerShooting playerShooting;

    [Header("ui stuffs")]
    public GameObject playerUI;

    public delegate void ResourceBarAction();
    public static event ResourceBarAction SetMaxStats, OnHealthChange, OnShieldChange;          //assigned in playerui.cs

    public delegate void InfoChange();
    public static event InfoChange OnInfoChange;

    [Header("player data")]
    public PlayerData data;
    // public PlayerData startingData;

    float healthDamageTime, shieldDamageTime, shieldBreakTime;
    float shieldResetRate;
    public bool shieldRecharging, shieldBroken, shieldResetting = false;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("player starting");
        data.currentHealth = data.startingHealth;
        data.currentShield = data.startingShield;

        // data.currentPrimaryAmmo = ;
        data.isInvincible = data.healthInvincible= data.isMoving= data.isDashing= data.dashLag= data.dashInvincible= data.isShooting= data.isAbsorbing = false;
        data.numScrap = 0;

        playerMovement.Setup();
        playerShooting.Setup();
        SetMaxStats();
    }

    void PlayerDataSetup(){
    }

    // Update is called once per frame
    void Update()
    {
        if(data.currentHealth <= 0){
            this.gameObject.SetActive(false);
        }
        
        if(shieldBroken){
            if(Time.time - shieldDamageTime >= data.shieldBreakDelay){
                shieldBroken = false;
                shieldRecharging = true;
            }
            else
                shieldRecharging = false;
        }
        else if(data.currentShield <= data.maxShield){
            if(Time.time - shieldDamageTime >= data.shieldRechargeDelay){
                shieldRecharging = true;
            }
            else
                shieldRecharging = false;
        }

        if(shieldResetting){
            ResetShield();
        }
        else if(shieldRecharging){
            RechargeShield();
        }

        if(Time.time - healthDamageTime >= data.damageInvincibleTime){
            data.healthInvincible = false;
        }

        if(!data.healthInvincible && !data.dashInvincible){
            data.isInvincible = false;
        }

        // Debug.Log(data.currentShield);
        OnShieldChange();
        OnHealthChange();

        playerMovement.UpdateMovement();
        playerShooting.UpdateShooting();
        playerController.UpdateSprite();
    }

    public void RechargeShield(){
        data.currentShield += data.shieldRechargeRate * Time.deltaTime;
        if(data.currentShield > data.maxShield){
            data.currentShield = data.maxShield;
            shieldRecharging = false;
        }
        // Debug.Log(data.currentShield);
        // OnShieldChange();
    }
    public void ResetShield(){
        data.currentShield += shieldResetRate * Time.deltaTime;
        if(data.currentShield > data.maxShield){
            data.currentShield = data.maxShield;
            shieldResetting = false;
        }
        // OnShieldChange();
    }

    //Handles what happens when player encounters damage
    public void TakeDamage(int damage){
        if(!shieldBroken){
            shieldDamageTime = Time.time;
            // Debug.Log("taking shield damge");
            data.currentShield -= damage;
            if(data.currentShield <= 0){
                Debug.Log("shield broken");
                data.currentShield = 0;
                shieldBroken = true;
                shieldBreakTime = Time.time;
            }
        }
        else{
            Debug.Log("taking health damge");
            data.currentHealth --;
            healthDamageTime = Time.time;
            data.healthInvincible = true;
            data.isInvincible = true;

            
            shieldRecharging = false;
            shieldBroken = false;
            shieldResetting = true;

            shieldResetRate = data.maxShield / data.damageInvincibleTime;
        }
    }
}
