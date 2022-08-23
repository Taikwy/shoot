using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Curve Line Data", menuName = "Curve Line Data")]
public class CurveLineData : ScriptableObject
{
    public LineData lineData;
    public string lineConditionType;                           //distance travelled || time
    public float lineCondition;
    
    public CurveData curveData;
    public float curveAngleCondition;

    public bool mirrored;
}
