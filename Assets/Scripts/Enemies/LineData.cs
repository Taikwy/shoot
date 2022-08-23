using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Line Data -", menuName = "Line Data")]
public class LineData : ScriptableObject
{
    public float movementSpeed;

    [Header("Line Info")]
    public float direction;
}