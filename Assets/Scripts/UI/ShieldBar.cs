using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : ResourceBar
{
    public PlayerData playerData;
    public void UpdateShield(){
        slider.value = playerData.currentShield;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        // Debug.Log(gameObject.name +  "updating shield " +  slider.maxValue);
    }

    public void SetMaxShield(){
        slider.maxValue = playerData.maxShield;
        slider.value = playerData.currentShield;
        fill.color = gradient.Evaluate(1f);
        // Debug.Log("setting max shield " + slider.maxValue + " " + playerData.maxShield);
    }

    public void OnDestroy(){
        // Debug.Log("destroyed");
        // Debug.Log(UnityEngine.StackTraceUtility.ExtractStackTrace()); 
    }
    public void OnDisable(){
        // Debug.Log("disabled");
        // Debug.Log(UnityEngine.StackTraceUtility.ExtractStackTrace()); 
    }
}
