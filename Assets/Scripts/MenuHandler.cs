using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    //Starts the game from the menu
    public void StartGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    //Quits the game from the menu
    public void QuitGame()
    {
        Application.Quit();
    }
}
