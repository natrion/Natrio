﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smalldata : MonoBehaviour
{

    public GameObject copyChunk;

    public GameObject oakTree;
    public GameObject berryTree;

    public GameObject oakBush;
    public GameObject berryBush;

    public GameObject oakSmallBushes;
    public GameObject berrySmallBushes;

    public GameObject grass1;
    public GameObject grass2;

    public GameObject poppy1;
    public GameObject poppy2;



    public GameObject Axe;
    public GameObject wheelbarrow;
    public GameObject OakLog;
    public GameObject berryLog;
    public GameObject Leaves;
    public GameObject BerryLeaves;
    public GameObject Apple;
    public GameObject PurpleBerry;
    public GameObject BlueBerry;

    public GameObject ItemsFolder;
    public GameObject PlayerFolder;
    public GameObject GrassFolder;

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
                    if (gd.world[i].type == 1)
                    { itemcopy = Instantiate(copyChunk); }

                    if (gd.world[i].type == 2)
                    { itemcopy = Instantiate(oakTree); }

                    if (gd.world[i].type == 3)
                    { itemcopy = Instantiate(oakBush); }

                    if (gd.world[i].type == 4)
                    { itemcopy = Instantiate(oakSmallBushes); }

                    if (gd.world[i].type == 5)
                    { itemcopy = Instantiate(grass1); }

                    if (gd.world[i].type == 6)
                    { itemcopy = Instantiate(poppy1); }

                    if (gd.world[i].type == 7)
                    { itemcopy = Instantiate(berryTree); }

                    if (gd.world[i].type == 8)
                    { itemcopy = Instantiate(berryBush); }

                    if (gd.world[i].type == 9)
                    { itemcopy = Instantiate(berrySmallBushes); }

                    if (gd.world[i].type == 10)
                    { itemcopy = Instantiate(grass2); }

                    if (gd.world[i].type == 11)
                    { itemcopy = Instantiate(poppy2); }

                    itemcopy.transform.position = new Vector3(gd.world[i].position[0], gd.world[i].position[1], 0);
                    itemcopy.transform.rotation = Quaternion.Euler(0, 0, gd.world[i].rotation);
                    itemcopy.transform.parent = GrassFolder.transform;
                }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////items
        for (int o = 0; o < gd.items.Length; o++)
        {
            if (gd.items[o] !=null )

            if (gd.items[o].type != 0 )
            {
                if (gd.items[o].type == 1)
                { itemcopy = Instantiate(Axe); }

                if (gd.items[o].type == 2)
                { itemcopy = Instantiate(wheelbarrow); }

                if (gd.items[o].type == 3)
                { itemcopy = Instantiate(OakLog); }

                if (gd.items[o].type == 4)
                { itemcopy = Instantiate(berryLog); }

                if (gd.items[o].type == 5)
                { itemcopy = Instantiate(Leaves); }

                if (gd.items[o].type == 6)
                { itemcopy = Instantiate(BerryLeaves); }

                if (gd.items[o].type == 7)
                { itemcopy = Instantiate(Apple); }

                if (gd.items[o].type == 8)
                { itemcopy = Instantiate(PurpleBerry); }

                if (gd.items[o].type == 9)
                { itemcopy = Instantiate(BlueBerry); }

                if (gd.items[o].typeFolder == 0)
                { itemcopy.transform.parent = ItemsFolder.transform; }

                if (gd.items[o].typeFolder == 1)
                { itemcopy.transform.parent = ItemsFolder.transform; }

                if (gd.items[o].typeFolder == 2)
                { itemcopy.transform.parent = coinsobject.foldersell.transform; }

                if (gd.items[o].typeFolder == 3)
                { itemcopy.transform.parent = ItemsFolder.transform; }

                itemcopy.transform.position = new Vector3(gd.items[o].position[0], gd.items[o].position[1], 0);
                itemcopy.transform.rotation = Quaternion.Euler(0, 0, gd.items[o].rotation);
            }
        }
        seedobject.StartChunk();
        print("StartChunk");
        
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
