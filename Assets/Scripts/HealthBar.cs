using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthImage;
    [SerializeField] private Text healthText;
    [SerializeField] private Image healthTextBackground;
    [SerializeField] private Gradient healthGradient;

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
}