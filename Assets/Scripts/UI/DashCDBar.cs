using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCDBar : ResourceBar
{
    public PlayerData playerData;
    // public FloatData healthData;

    public void UpdateCooldown(){
        slider.value = playerData.currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxCooldown(){
        slider.maxValue = playerData.maxHealth;
        slider.value = playerData.currentHealth;
        fill.color = gradient.Evaluate(1f);
    }
}
