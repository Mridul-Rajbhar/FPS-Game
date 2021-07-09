using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public GameObject startMenu, rulesMenu;

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }



    // Update is called once per frame
    public void Rules()
    {
        startMenu.SetActive(false);
        rulesMenu.SetActive(true);
    }

    public void Back()
    {
        startMenu.SetActive(true);
        rulesMenu.SetActive(false);
    }
}
