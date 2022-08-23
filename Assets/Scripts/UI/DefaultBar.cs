using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image border;
    public Image background;
    public Image fill;

    public void SetMaxAmmo(float maxAmmo){
        slider.maxValue = maxAmmo;
        slider.value = maxAmmo;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetCurrentAmmo(float currentAmmo){
        slider.value = currentAmmo;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update(){}
}
