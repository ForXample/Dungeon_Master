using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmeyAttack : MonoBehaviour
{
    protected GameObject hero;
    public virtual void Start()
    {
        hero = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
