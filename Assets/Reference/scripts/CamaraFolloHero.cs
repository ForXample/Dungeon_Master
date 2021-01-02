using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFolloHero : MonoBehaviour
{
    public Transform hero; //passing in hero's position 
    public float smoothing;
    public Vector3 offset; // have offset with hero on z axis

    private void FixedUpdate()
    {
        //check if player whether dead
        if(hero != null)
        {   
            //create smooth transition in here, lerp takes three var(currentPos, willGoPos, percentage for smooth) 
            Vector3 newPosition = Vector3.Lerp(transform.position, hero.transform.position - offset, smoothing);
            transform.position = newPosition;
            //Debug.Log(transform.position);    
        }

    }
}
 