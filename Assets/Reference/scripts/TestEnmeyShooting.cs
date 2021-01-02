using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnmeyShooting : EnmeyAttack
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;   
    public Vector3 offset;

    public float cooldown;


    public override void Start()
    {
        base.Start();
        StartCoroutine(ShootPlayer());
    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(cooldown);
        if (hero != null)
        {   
            GameObject spell = Instantiate(projectile, transform.position + offset, Quaternion.identity);
            Vector2 heroPosition = hero.transform.position;
            Debug.Log(heroPosition);
            Vector2 enmeyPosition = transform.position;
            Vector2 direction = (heroPosition - enmeyPosition).normalized;

            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<TestEnemyProjectile>().damage = Random.Range(minDamage, maxDamage);
            print("SHOT HERO");
            StartCoroutine(ShootPlayer());
        }
        else
        {
            print("CANT FIND HERO");
        }
    } 
}
