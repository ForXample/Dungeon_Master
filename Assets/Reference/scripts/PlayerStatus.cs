using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus playerStatus; //static 可以让这个成为唯一的一个，容易引用
    public GameObject hero;

    public Text healthNum;
    public Slider healthSlider;

    public float health;
    public float maxHealth; 

    //coins
    public int coins;
    public int gems;
    public Text cointsValues;
    public Text gemsValues;

    private void Awake()
    {
        if(playerStatus != null) //check whether there is a playerstatus exit
        {
            Destroy(playerStatus);
        }
        else
        {
            playerStatus = this;

        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        health = maxHealth;
        healthSlider.value = CalculateHealthPercentage(); //inital to 1 when it's full
    }

    public void HandleDamage(float damage)
    {
        health -= damage;
        CheckHealth();
        healthSlider.value = CalculateHealthPercentage();
    }

    public void HandleCharacter (float heal) 
    {
        health += heal;
        CheckOverheal();
        healthSlider.value = CalculateHealthPercentage();
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            health = 0;
            Destroy(hero);
        }
    }

    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    private float CalculateHealthPercentage()
    {
        healthNum.text = Mathf.Ceil(health).ToString() + "/" + Mathf.Ceil(maxHealth).ToString();
        return (health / maxHealth);
    }

    public void AddDrop(CoinPickUp drop)
    {
        if(drop.curentObject == CoinPickUp.PickupObject.coin)
        {
            coins += drop.pickUpQuantity;
            cointsValues.text = "Coins: " + coins.ToString();
        }
        else if(drop.curentObject == CoinPickUp.PickupObject.gem)
        {
            gems += drop.pickUpQuantity;
            gemsValues.text = "Gem: " + gems.ToString();
        }
    }

}
