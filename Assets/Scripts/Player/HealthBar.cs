using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    private float health; 
    private float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        maxHealth = FindObjectOfType<PlayerHealth>().GetMaxHealth();
        healthBar.minValue = 0;
        healthBar.maxValue = maxHealth;
        health = FindObjectOfType<PlayerHealth>().GetHealth();
        if (health <= 0)
        {
            healthBar.value = 0;
        }
        else
        {
            healthBar.value = health;
        }
    }
}
