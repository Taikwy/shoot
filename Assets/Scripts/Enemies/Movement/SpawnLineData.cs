using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Line Data", menuName = "Spawn Line Data")]
public class SpawnLineData : ScriptableObject
{
    public LineData lineData;
    public string lineConditionType;                           //distance travelled || time
    public float lineCondition;
    public float stopCondition;                                //Can only be time

    public Vector2 spawnOffset;
    
    public float distanceToActivation;
}
