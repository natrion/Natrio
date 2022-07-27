using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPlayCreateButton : MonoBehaviour
{
    public static bool Restart;
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void QuitGame()
    {
        Restart = false;
        Application.Quit();
    }
    public void CreateNewGame()
    {
        Restart = true;
        SceneManager.LoadScene("Game");
    }
}
