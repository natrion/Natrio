using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.Netcode.Transports.UTP;
using UnityEngine.UI;
using TMPro;
public class QuitPlayCreateButton : MonoBehaviour
{
    public GameObject AdressAndportChangeWindow;
    public static bool joinToAtheGame;
    public static bool Restart;
    public GameObject CreateNewWorldWindow;
    public GameObject CreateNewWorldButton;
    private static bool PlaingFirstTime;

    void Start()
    {
        AdressAndportChangeWindow.SetActive(false);
        joinToAtheGame = false;
        CreateNewWorldWindow.SetActive(false);
        if (File.Exists(SL.getFileName()))
        {
            PlaingFirstTime = false;          
        }
        else 
        {           
            PlaingFirstTime = true;
            CreateNewWorldButton.SetActive(false);   
        }
    }
    public void PlayGame()
    {
        if (PlaingFirstTime == true)
        {
            Restart = true;
            CreateNewGame();     
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
    public static string ChangeAdress;
    public static string ChangePort;

    public void OpenAdressAndportChangeWindow()
    {
        AdressAndportChangeWindow.SetActive(true);
    }
    public void CloseAdressAndportChangeWindow()
    {
        AdressAndportChangeWindow.SetActive(false);
    }
    public void OnChangeAdress()
    {
        ChangeAdress = AdressAndportChangeWindow.transform.GetChild(0).GetComponent<TMP_InputField>().text;
    }
    public void OnChangePort()
    {       
        ChangePort = AdressAndportChangeWindow.transform.GetChild(1).GetComponent<TMP_InputField>().text;
    }
    public void JoinToGame()
    {
        joinToAtheGame = true;
        SceneManager.LoadScene("Game");
    }
}

