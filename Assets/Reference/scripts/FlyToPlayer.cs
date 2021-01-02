using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToPlayer : MonoBehaviour
{
    private GameObject hero;
    public float speedTowardsHero;

    private void Start()
    {
        hero = GameObject.Find("Hero");
    }

    private void Update()
    {
        if(hero != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, hero.transform.position, speedTowardsHero * Time.deltaTime);
        }

    }
}
