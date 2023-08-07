using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using Unity.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using System.IO.Compression;

public class NetworkComunicator : NetworkBehaviour
{
    [SerializeField] Transform ItemFolder;
    [SerializeField] Transform NatureFolder;
    [SerializeField] smalldata Smalldata;
    [SerializeField] sellthings Sellthings;
    [SerializeField] private GameObject CopyPlayer;

    [SerializeField] private List<GameObject> Players = new List<GameObject>();
    [SerializeField] private List<bool> ConectedPlayers = new List<bool>();


    public int NuberOfPlayersEver;

    public int playernumber;

    public void StartServer()
    {
        Players.Add(null);
        ConectedPlayers.Add(true);
    }

    public void StartConection()
    {
        StartCoroutine(StartConectionWait());
    }
    public void StopConection()
    {
        GameStartServerRpc(false, playernumber);
    }
    private IEnumerator StartConectionWait()
    {
        yield return new WaitForSeconds(2);

        GameStartServerRpc(true, -1);
    }
    [ServerRpc(RequireOwnership = false)]
    public void GameStartServerRpc(bool CreatePlayer, int DestroyWhatplayer)
    {
        NuberOfPlayersEver++;

        if (CreatePlayer == true)
        { Players.Add(Instantiate(CopyPlayer)); ConectedPlayers.Add(true); }
        else { Destroy(Players[DestroyWhatplayer]); ConectedPlayers[DestroyWhatplayer] =false; }
        GameStartClientRpc(NuberOfPlayersEver, CreatePlayer, DestroyWhatplayer, PackConectedPlayers(ConectedPlayers), Packamedata(new gamedata(Smalldata, Sellthings)));
    }

/*    private string ConectedPlayersString()
    {
        BinaryFormatter formatter = new();
        StringStream stream = new StringStream();
        formatter.Serialize(stream, ConectedPlayers);
        string Result = stream.Text;
        stream.Close();
        return Result;
    }
    private void String_To_ConectedPlayers(string StringToConvert)
    {
        BinaryFormatter formatter = new();
        StringStream stream = new StringStream();
        stream.Text = StringToConvert;
        stream.Position = 0;
        ConectedPlayers = formatter.Deserialize(stream) as List<bool>;
        stream.Close();
    }*/
    [ClientRpc]
    private void GameStartClientRpc(int NuberOfPlayersEverOnServer , bool CreatePlayer, int DestroyWhatplayer,string ConectedPlayersString,string ConvertedGameData)
    {
        Smalldata.load(UnpackGamedata(ConvertedGameData));
        ConectedPlayers = UnpackConectedPlayers(ConectedPlayersString);
        NuberOfPlayersEver = NuberOfPlayersEverOnServer;

        if (playernumber == 0 )
        {
            playernumber = NuberOfPlayersEver;
            for (int i = 0; i < NuberOfPlayersEver; i++)
            {
                if (ConectedPlayers[i] ==true)
                {
                    Players.Add(Instantiate(CopyPlayer));
                }
                else Players.Add(null);
            }
            Players.Add(null);
        }
        else
        {
            if (CreatePlayer == true)
            {
                Players.Add(Instantiate(CopyPlayer));
            }
            else Destroy(Players[DestroyWhatplayer]);
        }
    }

    /// <summary>
    /// ///////////////////////////////////////////////////////////
    /// </summary>
    /// 
    [SerializeField] private Transform Player;
    public void SentInformation(bool movePlayer ,bool CreateThing, bool destroyThing, Vector3 position, Vector2 type, int fileOrder)
    {
        if (IsServer == true)
        {
            GameClientRpc(Player.position, playernumber, movePlayer, CreateThing, destroyThing, position, type, fileOrder);
        }
        else
        {
            GameServerRpc(Player.position, playernumber, movePlayer, CreateThing, destroyThing, position, type, fileOrder);           
        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void GameServerRpc(Vector3 PlayerPosition, int PlayerNumber, bool movePlayer, bool CreateThing, bool destroyThing, Vector3 position, Vector2 type, int fileOrder)// x: item or nature Object y: item type
    {
        if (CreateThing == true)
        {
            GameObject objectToCopy;
            if (type.x == 0)
            {
                 objectToCopy = Instantiate(Smalldata.Nature[(int)type.y]);

            }
            else  objectToCopy = Instantiate(Smalldata.Items[(int)type.y]);
            objectToCopy.transform.position = position;
        }
        if (destroyThing == true)
        {
            if (type.x == 0)
            {
                Destroy(NatureFolder.GetChild(fileOrder));

            }
            else Destroy(ItemFolder.GetChild(fileOrder));
        }
        if (movePlayer == true) Players[PlayerNumber].transform.position = PlayerPosition;

        GameClientRpc(PlayerPosition, PlayerNumber, movePlayer, CreateThing,destroyThing, position, type,fileOrder);
    }
    [ClientRpc]
    private void GameClientRpc(Vector3 PlayerPosition, int PlayerNumber, bool movePlayer, bool CreateThing, bool destroyThing, Vector3 position, Vector2 type,int fileOrder)
    {
        if (CreateThing == true)
        {
            GameObject objectToCopy;
            if (type.x == 0)
            {
                objectToCopy = Instantiate(Smalldata.Nature[(int)type.y]);
                objectToCopy.transform.parent = NatureFolder;
            }
            else { objectToCopy = Instantiate(Smalldata.Items[(int)type.y]); objectToCopy.transform.parent = ItemFolder; }
            objectToCopy.transform.position = position;
        }
        if (destroyThing == true)
        {
            if (type.x == 0)
            {
               Destroy( NatureFolder.GetChild(fileOrder));

            }
            else Destroy(ItemFolder.GetChild(fileOrder));
        }
        if (movePlayer == true) Players[PlayerNumber].transform.position = PlayerPosition;
    }
    
    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public string PackConectedPlayers(List<bool> _message)
    {
        using (MemoryStream input = new MemoryStream())
        {
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(input, _message);
            input.Seek(0, SeekOrigin.Begin);

            using (MemoryStream output = new MemoryStream())
            using (DeflateStream deflateStream = new DeflateStream(output, CompressionMode.Compress))
            {
                input.CopyTo(deflateStream);
                deflateStream.Close();

                return Convert.ToBase64String(output.ToArray());
            }
        }
    }

    public List<bool> UnpackConectedPlayers(string _packed)
    {
        using (MemoryStream input = new MemoryStream(Convert.FromBase64String(_packed)))
        using (DeflateStream deflateStream = new DeflateStream(input, CompressionMode.Decompress))
        using (MemoryStream output = new MemoryStream())
        {
            deflateStream.CopyTo(output);
            deflateStream.Close();
            output.Seek(0, SeekOrigin.Begin);

            BinaryFormatter bformatter = new BinaryFormatter();
            List<bool> message = (List<bool>)bformatter.Deserialize(output);
            return message;
        }
    }

    /// <summary>
    /// ////////////////////////////////////////////
    /// </summary>
    /// <param name="_message"></param>
    /// <returns></returns>
    public string Packamedata(gamedata _message)
    {
        using (MemoryStream input = new MemoryStream())
        {
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(input, _message);
            input.Seek(0, SeekOrigin.Begin);

            using (MemoryStream output = new MemoryStream())
            using (DeflateStream deflateStream = new DeflateStream(output, CompressionMode.Compress))
            {
                input.CopyTo(deflateStream);
                deflateStream.Close();

                return Convert.ToBase64String(output.ToArray());
            }
        }
    }

    public gamedata UnpackGamedata(string _packed)
    {
        using (MemoryStream input = new MemoryStream(Convert.FromBase64String(_packed)))
        using (DeflateStream deflateStream = new DeflateStream(input, CompressionMode.Decompress))
        using (MemoryStream output = new MemoryStream())
        {
            deflateStream.CopyTo(output);
            deflateStream.Close();
            output.Seek(0, SeekOrigin.Begin);

            BinaryFormatter bformatter = new BinaryFormatter();
            gamedata message = (gamedata)bformatter.Deserialize(output);
            return message;
        }
    }
}
