using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("movement info")]
    public float movementSpeed;
    public float dashDistance;
    public float dashSpeed;
    public bool isDashing = false;

    [Header("body info")]
    public int maxHealth;
    public int startingHealth;
    public int currentHealth;
    // public HealthBar healthBar;


    [Header("shooting info")]
    //shooting stuff
    public List<Gun> guns = new List<Gun>();
    public float maxPrimaryAmmo;
    public float currentPrimaryAmmo;
}
