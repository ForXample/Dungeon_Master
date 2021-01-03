﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorUp, doorDown;

    public bool roomLeft, roomRight, roomUp, roomDown;
    public Text text;
    public int stepToStart;
    public int doorNumber;

    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

    // Update is called once per frame
    public void UpdateRoom(float xOffset, float yOffset)
    {
<<<<<<< HEAD

=======
>>>>>>> 74543a5 (【fixBug】修复好了因为脚本编译器不同而导致的本地设置的丢失)
        stepToStart = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / yOffset));

        text.text = stepToStart.ToString();

        if (roomUp)
        {
            doorNumber++;
        }
        if (roomDown)
        {
            doorNumber++;
        }
        if (roomLeft)
        {
            doorNumber++;
        }
        if (roomRight)
        {
            doorNumber++;
        }
    }
}
