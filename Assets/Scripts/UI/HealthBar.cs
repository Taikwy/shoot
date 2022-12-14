using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : ResourceBar
{
    public PlayerData playerData;
    // public FloatData healthData;

    public void UpdateHealth(){
        slider.value = playerData.currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(){
        slider.maxValue = playerData.maxHealth;
        slider.value = playerData.currentHealth;
        fill.color = gradient.Evaluate(1f);
    }
}
