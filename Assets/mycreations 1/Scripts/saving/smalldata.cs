﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smalldata : MonoBehaviour
{

    public GameObject[] Nature;

    public GameObject[] Items;

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
    

    void Start()
    {
        if(QuitPlayCreateButton.Restart == true)
        {
            sellScript.Startcript();
            Restart();

        }
        else
        {
            load();
        }
        
    }
    public void save()
    {
        SL.saveGame(new gamedata(this, coinsobject));
    }


    public void load()
    {

        gamedata gd = SL.loadGame();
        
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

                if(gd.items[o].type == 23)
                {
                    itemcopy.transform.GetChild(1).GetComponent<TextMesh>().text = gd.items[o].text;
                }

                if(gd.items[o].type == 15 | gd.items[o].type == 22| gd.items[o].type == 33 | gd.items[o].type == 35)
                {
                    itemcopy.GetComponent<ItemCreator>().On = gd.items[o].ItemCreatorOn;
                }

                if (gd.items[o].type == 15 | gd.items[o].type == 22 | gd.items[o].type == 23 | gd.items[o].type == 33 | gd.items[o].type == 35)
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
