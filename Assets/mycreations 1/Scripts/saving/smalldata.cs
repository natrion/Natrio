using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class smalldata : MonoBehaviour
{

    public GameObject[] Nature;

    public GameObject[] Items;

    public bool[] ItemsThatCanBeCrerating;

    public bool[] ItemsToNotRotate;

    public Transform[] Folders;

    public GameObject ItemsFolder;
    public GameObject PlayerFolder;
    public GameObject GrassFolder;


    public sellthings sellScript;
    public float seedOfLastGame;
    public Transform player;
    public sellthings coinsobject;
    public PG seedobject;
    private float seedchanger = 100f;
    private float PerlinNoise;
    private GameObject itemcopy;
    public int OnItem = 0;
    public GameObject SaveButton;
    public GameObject QuitNotSaveButton;
    public GameObject StartServerButton;
    public  NetworkComunicator NetworkComunicatorScript;

    void Start()
    {
        if (QuitPlayCreateButton.joinToAtheGame == false)
        {
            if (QuitPlayCreateButton.Restart == true)
            {
                sellScript.Startcript();
                Restart();

            }
            else
            {
                load(SL.loadGame());
            }
        }
        else
        {
            NetworkManager.Singleton.StartClient();
            SaveButton.SetActive(false);
            QuitNotSaveButton.SetActive(false);
            StartServerButton.SetActive(false);          
            NetworkComunicatorScript.StartConection();
        }
    }
    public void save()
    {
        SL.saveGame(new gamedata(this, coinsobject));
    }

    public void load(gamedata gd)
    {
        int childnumber = seedobject.transform.childCount;

        for (int i = 0; i < childnumber; i++)
        {
            Transform childi = seedobject.transform.GetChild(i);
            Destroy(childi.gameObject);
        }
  
        sellScript.autoseling = gd.autoseling;
        sellScript.Startcript();
        seedOfLastGame = gd.seed;
        transform.parent.position = new Vector3(gd.playerPosition[0], gd.playerPosition[1], 0);
        coinsobject.how_many_coins = gd.playerCoins;
        FindObjectOfType<showingtext>().showtext(coinsobject.how_many_coins);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////chunks
        for (int i = 0; i < gd.world.Length; i++)
        {
            if (gd.world[i] != null)

                if (gd.world[i].type != 0)
                {
                    itemcopy = Instantiate(Nature[gd.world[i].type]);

                    itemcopy.transform.position = new Vector3(gd.world[i].position[0], gd.world[i].position[1], 0);
                    itemcopy.transform.eulerAngles = new Vector3(0, 0, gd.world[i].rotation);
                    itemcopy.transform.parent = GrassFolder.transform;
                    itemcopy.GetComponent<SpriteRenderer>().sortingOrder = gd.world[i].Layer;
                }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////items
        for (int o = 0; o < gd.items.Length; o++)
        {
            if (gd.items[o] !=null )

            if (gd.items[o].type != 0 )
            {
                itemcopy = Instantiate(Items[gd.items[o].type]);

                    /////////////////////////////////////////////////////////filesTypes
                itemcopy.transform.parent = Folders[gd.items[o].typeFolder];

                if (gd.items[o].typeFolder == 3)
                        itemcopy.transform.parent = Folders[1];


                if (gd.items[o].type == 23)
                {
                    itemcopy.transform.GetChild(1).GetComponent<TextMesh>().text = gd.items[o].text;
                }

                if(ItemsThatCanBeCrerating[gd.items[o].type]  == true)
                {
                    itemcopy.GetComponent<ItemCreator>().On = gd.items[o].ItemCreatorOn;
                }

                if (ItemsToNotRotate[gd.items[o].type] == true)
                {
                    itemcopy.transform.GetChild(0).eulerAngles = new Vector3(0, 0, gd.items[o].rotation);
                    
                }
                else
                {                    
                    itemcopy.transform.eulerAngles = new Vector3(0, 0, gd.items[o].rotation);
                } 

                itemcopy.transform.position = new Vector3(gd.items[o].position[0], gd.items[o].position[1], 0);
            }
        }
        seedobject.StartChunk();
        
        
    }
 
    public void Restart()
    {

        coinsobject.how_many_coins = 0;

        FindObjectOfType<showingtext>().showtext(coinsobject.how_many_coins);

        transform.parent.position= new Vector3(0, 0, 0);

        bool done = false;
        seedOfLastGame = Random.Range(1f, 4000f);
        while (done == false)
        {
            if (PerlinNoise < 0.3f)
            {
                seedchanger = 4.444f;
            }else
            {
                seedchanger = 111.111f;
            }
            
            seedOfLastGame += seedchanger;
            PerlinNoise = Mathf.PerlinNoise((seedobject.size / seedobject.biom_Diversity) + seedOfLastGame, (seedobject.size / seedobject.biom_Diversity) + seedOfLastGame);
            if (PerlinNoise < 0.1f)
            {
                done = true;
            }
        }
        int childnumber = seedobject.transform.childCount;
        for (int i = 0; i < childnumber; i++)
        {
            Transform childi = seedobject.transform.GetChild(i);
            Destroy(childi.gameObject);
        }
        seedobject.StartChunk();
    }
}
