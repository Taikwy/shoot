using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("KEYBINDS")]
    public string dashKey;
    public string absorbKey;
    public string primaryFireKey, specialFireKey, switchSpecialGunKey;

    [Header("health info")]
    public int maxHealth;
    public int startingHealth, currentHealth;
    public float damageInvincibleTime;
    // public float health
    //public float healthBombRadius;            //ADD THIS LATER WHEN BOMBS ARE ENABLED

    [Header("shield info")]
    public float maxShield;
    public float startingShield, currentShield;
    public float shieldBreakDelay, shieldRechargeDelay, shieldRechargeRate;
    // public bool shieldRecharging, shieldBroken = false;
    //public float shieldBombRadius;            //ADD THIS LATER WHEN BOMBS ARE ENABLED

    [Header("movement info")]
    public float movementSpeed;
    public float dashDistance, dashTime, dashEndlagAmount;
    [HideInInspector] public float dashSpeed;
    
    [Header("dash aftgerimage info")]
    public GameObject afterImagePrefab;
    public float activeTime, startingAlpha, alphaMultiplier;
    public Color afterimageColor;
    public float distanceBetweenAfterimages, timeBetweenAfterimages;

    [Header("shooting info")]
    public List<Gun> guns = new List<Gun>();
    public float maxPrimaryAmmo, currentPrimaryAmmo;
    
    [Header("abosrbing info")]
    public float absorbMovementSpeed;

    [Header("player state info")]
    public bool isInvincible = false;
    public bool healthInvincible, isMoving, isDashing, dashLag, dashInvincible, isShooting, isAbsorbing = false;
}
