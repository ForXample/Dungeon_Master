using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pressAny : MonoBehaviour
{

    // Detects if any key has been pressed down.

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("A key or mouse click has been detected");
            SceneManager.LoadScene("start_menu");
        }
    }
}