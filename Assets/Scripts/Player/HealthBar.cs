using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    protected Slider healthSlider;

    [SerializeField]
    protected Slider shieldSlider;

    public void SetHealth(float health) {
        healthSlider.value = health;
    }
    public void SetShield(float shield) {
        shieldSlider.value = shield;
    }

    public void SetMaxHealth(float health) {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    public void SetMaxShield(float shield) {
        shieldSlider.maxValue = shield;
        shieldSlider.value = shield;
    }
}
