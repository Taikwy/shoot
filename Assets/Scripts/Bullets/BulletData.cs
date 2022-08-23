using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Data", menuName = "Bullet Data")]
public class BulletData : ScriptableObject
{
    [Header("Bullet Prefab Info")]
    public new string name;

    public Sprite defaultSprite;
    // public Rigidbody2D rb;
    // public BoxCollider2D collider;
    public float colliderXOffset, colliderYOffset, colliderWidth, colliderHeight;
    
    // public Animation animation;
    public RuntimeAnimatorController controller;
    // public AnimationClip animationClip;
    // public List<string> additionalScripts;
    
    
    [Header("Bullet Info")]
    public string bulletType;
    public int health;
    public int damage;
    public float movementSpeed;

    public float ammoCost;
    public float cooldown;
}
