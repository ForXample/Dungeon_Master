using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public enum PickupObject { coin, gem };
    public PickupObject curentObject;
    public int pickUpQuantity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Hero")
        {
            PlayerStatus.playerStatus.AddDrop(this);
            Destroy(gameObject);
        }

    }
} 
