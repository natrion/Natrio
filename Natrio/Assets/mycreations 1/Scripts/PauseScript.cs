using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    private bool paused =false;
    public GameObject PauseMenu;
    public GameObject NotSaveQuitWindow;
   
    void Start()
    {
        
        NotSaveQuitWindow.SetActive(false);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {        
            if (paused == true)
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1f;
                paused = false;
                NotSaveQuitWindow.SetActive(false);
                
            }
            else
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
                paused = true;
            }            
        }       
    }
    public void NotSaveQuitWindowOpen()
    {
        NotSaveQuitWindow.SetActive(true);
    }
    public void NotSaveQuitWindowClose()
    {
        NotSaveQuitWindow.SetActive(false);
    }
 
    public void UnPaused()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        NotSaveQuitWindow.SetActive(false);
      
    }
    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
