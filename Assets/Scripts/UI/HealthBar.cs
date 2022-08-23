using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image border;
    public Image background;
    public Image fill;

    public void SetMaxHealth(int maxHealth){
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetCurrentHealth(int currentHealth){
        slider.value = currentHealth;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update(){}
}
