using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
public class Lobby : MonoBehaviour
{
    // Start is called before the first frame update
    private Unity.Services.Lobbies.Models.Lobby HostLobby;
    private void Update()
    {
        SendhartBeet();
    }
    float HartBeetTme = 15;
    float hartBeetTimeMax = 15;
    void SendhartBeet()
    {
        if (HostLobby != null)
        {
            HartBeetTme -= Time.deltaTime;
            if (HartBeetTme<0)
            {
                HartBeetTme = hartBeetTimeMax;

                LobbyService.Instance.SendHeartbeatPingAsync(HostLobby.Id);
            }
        }
    }
    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("signedIn" + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    public async void createlobby()
    {
        if(HostLobby == null)
        try
        {
            Unity.Services.Lobbies.Models.Lobby lobby = await LobbyService.Instance.CreateLobbyAsync("my lobby", 4);
            Debug.Log("lobby created");
            HostLobby = lobby;
        }
        catch (LobbyServiceException e )
        {
            Debug.Log("e");
        }

    }
    public async void listLobbys()
    {
        try
        {
            Unity.Services.Lobbies.Models.QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();
            //queryResponse.Results
            Debug.Log("Lobbies found: " + queryResponse.Results.Count);

            foreach (Unity.Services.Lobbies.Models.Lobby lobby in queryResponse.Results)
            {
                Debug.Log("lobby:" + lobby.Name);
            }
        }
        catch (LobbyServiceException e )
        {
            Debug.Log(e);
        }
        
    }
    private async void JoinLobby()
    {
        try
        {
            Unity.Services.Lobbies.Models.QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Lobbies.Instance.JoinLobbyByIdAsync(queryResponse.Results[0].Id);
            //queryResponse.Results
            Debug.Log("Lobbies found: " + queryResponse.Results.Count);

            foreach (Unity.Services.Lobbies.Models.Lobby lobby in queryResponse.Results)
            {
                Debug.Log("lobby:" + lobby.Name);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

        
    }
}
