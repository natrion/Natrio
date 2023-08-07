using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPlayCreateButton : MonoBehaviour
{
    public static bool Restart;
    public GameObject CreateNewWorldWindow;
    void Start()
    {
        CreateNewWorldWindow.SetActive(false);
    }
    public void PlayGame()
    {
        Restart = false;
        SceneManager.LoadScene("Game");
    }
    
    public void QuitGame()
    {       
        Application.Quit();
    }
    public void CreateNewGame()
    {
        Restart = true;
        SceneManager.LoadScene("Game");
    }
    public void CreateNewWorldWindowOpen()
    {
        CreateNewWorldWindow.SetActive(true);
    }
    public void CreateNewWorldWindowClose()
    {
        CreateNewWorldWindow.SetActive(false);
    }
}

