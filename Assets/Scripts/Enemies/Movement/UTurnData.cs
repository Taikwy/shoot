using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New U Turn Data", menuName = "U Data Data")]
public class UTurnData : ScriptableObject
{
    public LineData lineData;
    public string lineConditionType;                           //distance travelled || time
    public float lineCondition;
    public float stopCondition;                                //Can only be time
}
