using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testProjectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name != "Hero" && collision.tag != "drop")
        {
            Destroy(gameObject);
        }
        if(collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().HandleDamage(damage);
        }
    }
}
