using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingPanel;
    //start button function
    public void StartGame()
    {
        Debug.Log("Entering to level_1....");
        SceneManager.LoadScene("Level_1");
    }

    //quit button function
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingPanel.SetActive(true);
    }

    public void CloseSettidngs()
    {
        settingPanel.SetActive(false);
    }
} 
