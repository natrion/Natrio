using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class UInetworkManager : MonoBehaviour
{
    [SerializeField] private Button ServerButton;

    private void Awake()
    {
        ServerButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
    }
}
