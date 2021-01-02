using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyProjectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy" && collision.tag != "drop")
        {
            if(collision.tag == "Hero")
            {
                PlayerStatus.playerStatus.HandleDamage(damage); 
            }
            Destroy(gameObject);
        }
    }
}
