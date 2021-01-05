using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public void PlayGame()
    {

        SceneManager.LoadScene("map_lzx01");

    }


    public void ExitGame()
    {

        Debug.Log("Now Quitting");
        Application.Quit();

    }
}
