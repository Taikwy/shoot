using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Curve Data -", menuName = "Curve Data")]
public class CurveData : ScriptableObject
{
    [Header("Curve Info")]
    // public Vector2 rotationCenter;
    public float radius, startingAngle;
    public bool clockwise;
    public bool mirrored;
}