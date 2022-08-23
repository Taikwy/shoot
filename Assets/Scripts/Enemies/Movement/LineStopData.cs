using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Line Stop Data", menuName = "Line Stop Data")]
public class LineStopData : ScriptableObject
{
    public LineData lineData;
    public string lineConditionType;                           //distance travelled || time
    public float lineCondition;
    public float stopCondition;                                //Can only be time
}
