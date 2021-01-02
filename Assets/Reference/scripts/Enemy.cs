using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healthBar;
    public Slider healthBarSlider;

    public GameObject lotDrop;

    private void Start()
    {
        health = maxHealth;
    }

    public void HandleDamage(float damage)
    {
        healthBar.SetActive(true);
        health -= damage;
        CheckHealth();
        healthBarSlider.value = CalculateHealthPercentage(); 
    }

    public void HandleCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    private void CheckHealth()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            Instantiate(lotDrop, transform.position, Quaternion.identity);
        }
    }

    private void CheckOverheal()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    } 

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }
}
