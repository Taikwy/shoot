using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zigzag Data -", menuName = "Zigzag Data")]
public class ZigzagData : ScriptableObject
{
    [Header("Zigzag Info")]
    public float mainAngle;
    public float subAngle;
    public bool flipped;

    public string directionConditionType;
    public float directionCondition;

    public string extraConditionType;
    // public string stayConditionType;
    public float stayCondition;
    public string curveConditionType;
    public float curveCondition;
    public CurveData curveData;





    [Header("Old Info")]
    public float zigAngle;
    public float zagAngle;            //In angles
    public float zigStayTime;
    
    [Header("Direction Change Info")]
    //Direction change conditions ------------------------------
    [Tooltip("distance, y distance, time")]
    public string directionChangeCondition;
    public bool changeAfterSet;                //Whether to change immediately or after full zigzag

    [Header("Direction Change Conditions")]
    //zigzag conditions --------------------------------------
    public float zigDistCondition;
    public float zigDistYCondition;
    public float zigTimeCondition;
}