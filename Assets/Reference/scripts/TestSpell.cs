using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) //left is 0, right is 1, middle is 2
        {
            //generate the spell 
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity); // insatntiate-> make something; (object to create, position, rotation) quaternion.identity-> default routation

            //calculate the direction where the mouse points to
            Vector2 myPosition = transform.position;                                     //get player position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get curremt mouse position
            Vector2 direction = (mousePosition - myPosition).normalized;                 //normailized -> chage magnitute into 1 and remain it's direction  
            
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<testProjectile>().damage = Random.Range(minDamage, maxDamage);

            Debug.Log("EMIT SPELL");
        }
         
    }

} 
 