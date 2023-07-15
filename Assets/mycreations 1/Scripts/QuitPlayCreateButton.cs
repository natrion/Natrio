using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class QuitPlayCreateButton : MonoBehaviour
{
    public static bool Restart;
    public GameObject CreateNewWorldWindow;
    private static bool PlaingFirstTime;

    void Start()
    {
        CreateNewWorldWindow.SetActive(false);
        if (File.Exists(SL.getFileName()))
        {
            PlaingFirstTime = false;
        }
        else 
        {
            PlaingFirstTime = true;

            if (gameObject.name == "NewWorldButton")
            {
                gameObject.SetActive(false);
            }
               
        }
        
            
        
    }
    public void PlayGame()
    {
        if (PlaingFirstTime == true)
        {
            QuitGame();     
        }else
        {
            Restart = false;
            SceneManager.LoadScene("Game");
        }
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

