using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthImage;

    public void UpdateHealth(float enemyHealth)
    {
        float healthPercent = enemyHealth / maxHealth;

        if (healthPercent < 0)
            healthPercent = 0;

        healthImage.fillAmount = healthPercent;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }
}
