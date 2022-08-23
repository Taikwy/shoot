using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    // public new string name;

    public float movementSpeed;
    public float dashDistance;

    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
}
