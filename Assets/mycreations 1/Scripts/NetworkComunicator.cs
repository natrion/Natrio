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
using UnityEngine.Networking;

public class NetworkComunicator : NetworkBehaviour
{
    [SerializeField] Transform ItemFolder;
    [SerializeField] Transform NatureFolder;
    [SerializeField] smalldata Smalldata;
    [SerializeField] sellthings Sellthings;
    [SerializeField] private GameObject CopyPlayer;

    [SerializeField] private List<GameObject> Players = new List<GameObject>();
    [SerializeField] private List<bool> ConectedPlayers = new List<bool>();

    [SerializeField] public List<GameObject> AllObjects;
    private int tatalAmount;

    IEnumerator StartAllObjects()
    {
        yield return null;
        tatalAmount = -1;
        AllObjects.Clear();
        for (int i = 0; i < ItemFolder.childCount; i++)
        {
            tatalAmount++;
            GameObject Child = ItemFolder.GetChild(i).gameObject;
            AllObjects.Add(Child);
            if (Child.GetComponent<dataHolder>() == null)
            {
                dataHolder DataHolder = Child.AddComponent(typeof(dataHolder)) as dataHolder;
                DataHolder.dataIdentificator = tatalAmount;
            } else  Child.GetComponent<dataHolder>().dataIdentificator = tatalAmount;  
        }
        for (int i = 0; i < NatureFolder.childCount; i++)
        {
            tatalAmount++;
            GameObject Child = NatureFolder.GetChild(i).gameObject;
            AllObjects.Add(Child);
            if (Child.GetComponent<dataHolder>() == null)
            {
                dataHolder DataHolder = Child.AddComponent(typeof(dataHolder)) as dataHolder;
                DataHolder.dataIdentificator = tatalAmount;
            }
            else Child.GetComponent<dataHolder>().dataIdentificator = tatalAmount;
        }
        for (int i = 0; i < Sellthings.foldersell.transform.childCount; i++)
        {
            tatalAmount++;
            GameObject Child = Sellthings.foldersell.transform.GetChild(i).gameObject;
            AllObjects.Add(Child);
            if (Child.GetComponent<dataHolder>() == null)
            {
                dataHolder DataHolder = Child.AddComponent(typeof(dataHolder)) as dataHolder;
                DataHolder.dataIdentificator = tatalAmount;
            }
            else Child.GetComponent<dataHolder>().dataIdentificator = tatalAmount;
        }
    }
    private int NuberOfPlayersEver;

    private int playernumber;

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
        yield return new WaitForSeconds(5f);

        GameStartServerRpc(true, -1);
    }

    [ServerRpc(RequireOwnership = false)]
    public void GameStartServerRpc(bool CreatePlayer, int DestroyWhatplayer)
    {
        StartCoroutine(StartAllObjects());
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
        StartCoroutine(StartAllObjects());
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
    private Vector2 type;
    public void SentInformation(bool movePlayer ,bool CreateThing, bool destroyThing ,int SellOnPlatform, Transform Thing)
    {
        if (NuberOfPlayersEver == 0) return;

 
        if (CreateThing ==true)
        {
            if (Thing.transform.parent == NatureFolder)
            {
                type.x = 0;
                for (byte i = 1; i < Smalldata.Nature.Length - 1; i++)
                {
                    if (Smalldata.Nature[i].CompareTag(Thing.gameObject.tag))
                    {
                        type.y = (float)i;
                    }
                }
            }
            else if(Thing.transform.parent == ItemFolder | Thing.transform.parent == Sellthings.foldersell.transform)
            {
                type.x = 1;
                for (byte i = 1; i < Smalldata.Items.Length - 1; i++)
                {
                    if (Smalldata.Items[i].CompareTag(Thing.gameObject.tag))
                    {
                        type.y = (float)i;
                    }
                }
            }
        }
        Vector3 Rotation;
        Vector3 position;
        int fileOrder;
        if (CreateThing == true)
        {
            Rotation = Thing.eulerAngles;
            position = Thing.position;
            if (type.x ==1){
                if (Smalldata.ItemsToNotRotate[(int)type.y] == true)
                {
                    Rotation = Thing.GetChild(0).eulerAngles;
                }
            }
            dataHolder DataHolder;
            if (Thing.GetComponent<dataHolder>() == null) { DataHolder = Thing.gameObject.AddComponent(typeof(dataHolder)) as dataHolder; }
            else DataHolder = Thing.GetComponent<dataHolder>();
            DataHolder.dataIdentificator = AllObjects.Count;
            AllObjects.Add(Thing.gameObject);

        }
        else
        {
            Rotation = new Vector3(0, 0, 0);
            position = new Vector3(0, 0, 0);
        }

        if (destroyThing == true)
        {
            fileOrder = Thing.GetComponent<dataHolder>().dataIdentificator;

            //AllObjects.Add(Thing.gameObject);
            //Thing.GetComponent<dataHolder>().dataIdentificator = AllObjects.Count-1;
        }
        else 
        {
            fileOrder = 0;
        }
        if (IsServer == true)
        {
            GameClientRpc(Player.position, playernumber, movePlayer, CreateThing, destroyThing, position, type, fileOrder, Rotation, SellOnPlatform);
        }
        else
        {
            GameServerRpc(Player.position, playernumber, movePlayer, CreateThing, destroyThing, position, type, fileOrder, Rotation, SellOnPlatform);           
        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void GameServerRpc(Vector3 PlayerPosition, int PlayerNumber, bool movePlayer, bool CreateThing, bool destroyThing, Vector3 position, Vector2 type, int fileOrder, Vector3 Rotation, int SellOnPlatform)// x: item or nature Object y: item type
    {
        if (SellOnPlatform == 1)
        {
            StartCoroutine(Sellthings.Autosell(true));
        }
        if (SellOnPlatform == 2)
        {
            Sellthings.seling = true;
            Sellthings.autoseling = true;
            StartCoroutine(Sellthings.Autosell(false));
        }
        if (SellOnPlatform == 3)
        {
            Sellthings.seling = false;
            Sellthings.autoseling = false;
        }
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
            if (type.x == 1)
            {
                if (Smalldata.ItemsToNotRotate[(int)type.y] == true)
                {
                    objectToCopy.transform.GetChild(0).eulerAngles = Rotation;
                }
                else objectToCopy.transform.eulerAngles = Rotation;
            }
            else objectToCopy.transform.eulerAngles = Rotation;

            AllObjects.Add(objectToCopy);

            dataHolder DataHolder = objectToCopy.AddComponent(typeof(dataHolder)) as dataHolder;
            DataHolder.dataIdentificator = AllObjects.Count;
        }
        if (destroyThing == true)
        {
            print(fileOrder);
            Destroy(AllObjects[fileOrder]);
        }

        if (movePlayer == true) Players[PlayerNumber].transform.position = PlayerPosition;

        GameClientRpc(PlayerPosition, PlayerNumber, movePlayer, CreateThing,destroyThing, position, type,fileOrder,Rotation,SellOnPlatform);
    }
    [ClientRpc]
    private void GameClientRpc(Vector3 PlayerPosition, int PlayerNumber, bool movePlayer, bool CreateThing, bool destroyThing, Vector3 position, Vector2 type,int fileOrder, Vector3 Rotation, int SellOnPlatform)
    {
        if (PlayerNumber == playernumber) return;


        if (SellOnPlatform == 1)
        {
            StartCoroutine(Sellthings.Autosell(true));
        }
        if (SellOnPlatform == 2)
        {
            Sellthings.seling = true;
            Sellthings.autoseling = true;
            StartCoroutine(Sellthings.Autosell(false));
        }
        if (SellOnPlatform == 3)
        {
            Sellthings.seling = false;
            Sellthings.autoseling = false;
        }
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
            if (type.x == 1)
            {
                if (Smalldata.ItemsToNotRotate[(int)type.y] == true)
                {
                    objectToCopy.transform.GetChild(0).eulerAngles = Rotation;
                }
                else objectToCopy.transform.eulerAngles = Rotation;
            }
            else objectToCopy.transform.eulerAngles = Rotation;

            AllObjects.Add(objectToCopy);

            dataHolder DataHolder = objectToCopy.AddComponent(typeof(dataHolder)) as dataHolder;
            DataHolder.dataIdentificator = AllObjects.Count;
        }
        if (destroyThing == true)
        {
            print(fileOrder);
            Destroy(AllObjects[fileOrder]);
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
