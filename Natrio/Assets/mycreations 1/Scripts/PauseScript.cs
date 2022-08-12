using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseScript : MonoBehaviour
{
    public static bool paused =false;
    public GameObject PauseMenu;
    public GameObject NotSaveQuitWindow;
    private AudioSource clickButtonAudio;

    void Start()
    {
        paused = false;
        clickButtonAudio = transform.GetComponent<AudioSource>();
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
        clickButtonAudio.Play();
        NotSaveQuitWindow.SetActive(true);
    }
    public void NotSaveQuitWindowClose()
    {
        clickButtonAudio.Play();
        NotSaveQuitWindow.SetActive(false);
    }
 
    public void UnPaused()
    {
        clickButtonAudio.Play();
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        NotSaveQuitWindow.SetActive(false);
    }
    public void Quit()
    {
        clickButtonAudio.Play();
        SceneManager.LoadScene("Menu");
    }
}
