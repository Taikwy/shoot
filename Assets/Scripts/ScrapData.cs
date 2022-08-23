using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scrap Data", menuName = "Scrap Data")]
public class ScrapData : ScriptableObject
{
    public new string name;

    public Sprite defaultSprite;
    public float colliderXOffset, colliderYOffset, colliderWidth, colliderHeight;
    
    public string ammoType;
    // public int ammoType;
    // public float amount;
    public float defaultAmount;
    public float skillAmount;

    public float activeTimer;
}