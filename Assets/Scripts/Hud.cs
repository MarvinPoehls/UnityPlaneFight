using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxStamina;
    [SerializeField] private Image healthImage;
    [SerializeField] private Image staminaImage;
    [SerializeField] private Text healthText;
    [SerializeField] private Image healthTextBackground;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image weaponIcon;
    [SerializeField] private Text coolDownText;
    [SerializeField] private Image coolDownCircle;
    private float maxCoolDown;

    public void UpdateHealth(float playerHealth)
    {
        float healthPercent = playerHealth / maxHealth;

        if (healthPercent < 0 )
            healthPercent = 0;

        healthImage.fillAmount = healthPercent;
        healthImage.color = healthGradient.Evaluate(healthPercent);
        healthTextBackground.color = healthGradient.Evaluate(healthPercent);
        healthText.text = Mathf.Round(healthPercent * 100).ToString();
    }

    public void UpdateStamina(float playerStamina)
    {
        float staminaPercent = playerStamina/maxStamina;

        if (staminaPercent < 0)
            staminaPercent = 0;

        staminaImage.fillAmount = staminaPercent;
    }

    public void SetWeaponSprite(Sprite weaponSprite)
    {
        weaponIcon.sprite = weaponSprite;
    }

    public void UpdateCooldown(float coolDown)
    {
        if (coolDown < 0) 
            coolDown = 0;

        coolDownText.text = Math.Round(coolDown, 2).ToString();
        coolDownCircle.fillAmount = 1 - coolDown/maxCoolDown;
    }

    public void SetMaxCooldown(float coolDown)
    {
        maxCoolDown = coolDown;
    }
}