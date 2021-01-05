using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class changeTextAlpha : MonoBehaviour {

    Text text;

    void Start ()
    {
        text = GetComponent<Text>();
        startBlinking();

    }

    IEnumerator Blink()
    {
        while (true)
        {
            switch(text.color.a.ToString())
            {
             case "0":
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

                yield return new WaitForSeconds(0.8f);
                break;
                
            case"1":
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

                yield return new WaitForSeconds(0.5f);
                break;

            }
        }



    }

    void startBlinking()
    {   
        StopCoroutine("Blink");
        StartCoroutine("Blink");

    }

    void stopBlinking()
    {
        StopCoroutine("Blink");

    }

}   