using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("player health bars")]
    public int maxHealth, startingHealth, currentHealth;
    // public float health
    //public float healthBombRadius;            //ADD THIS LATER WHEN BOMBS ARE ENABLED

    public int maxShield, startingShield, currentShield;
    public float shieldRechargeDelay, shieldRechargeRate;
    //public float shieldBombRadius;            //ADD THIS LATER WHEN BOMBS ARE ENABLED

    [Header("movement info")]
    public float movementSpeed;
    public float dashDistance;
    public float dashTime;
    public float dashEndlagAmount;
    [HideInInspector] public float dashSpeed;
    
    [Header("dash aftgerimage info")]
    public GameObject afterImagePrefab;
    public float activeTime, startingAlpha, alphaMultiplier;
    public Color afterimageColor;
    public float distanceBetweenAfterimages, timeBetweenAfterimages;

    [Header("shooting info")]
    public List<Gun> guns = new List<Gun>();
    public float maxPrimaryAmmo;
    public float currentPrimaryAmmo;

    
    [Header("abosrbing info")]
    public float absorbMovementSpeed;

    [Header("player state info")]
    public bool isDashing = false;
    public bool dashLag = false;
    public bool isInvincible = false;
    public bool isShooting = false;
    public bool isAbsorbing = false;
}
