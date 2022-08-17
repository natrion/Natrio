using System.Collections;
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

    public GameObject OldOakTree;
    public GameObject OldOakBush;
    public GameObject OldOaksmallbushes;
    public GameObject grass3;
    public GameObject poppy3;

    public GameObject Axe;
    public GameObject wheelbarrow;

    public GameObject AxeT2;
    public GameObject wheelbarrowT2;

    public GameObject wheelbarrowT3;

    public GameObject OakLog;
    public GameObject berryLog;

    public GameObject Leaves;
    public GameObject BerryLeaves;

    public GameObject Apple;
    public GameObject PurpleBerry;
    public GameObject BlueBerry;

    public GameObject OldOakLog;
    public GameObject OldOakleaves;

    public GameObject OakFarmPlot;
    public GameObject T1conveyorBelt;

    public GameObject T1conveyorBeltRight;
    public GameObject T1conveyorBeltLeft;

    public GameObject T2conveyorBelt;
    public GameObject T2conveyorBeltRight;
    public GameObject T2conveyorBeltLeft;
    public GameObject T2BerryFarmPlot;

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
                    //////////////////////////////////////
                    if (gd.world[i].type == 1)
                    { itemcopy = Instantiate(copyChunk); }
                    //////////////////////////////////////////////oakbiom
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
                    ///////////////////////////////////////////////berryBiom

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
                    ///////////////////////////////////////////////OldOakBiom
                    
                    if (gd.world[i].type == 12)
                    { itemcopy = Instantiate(OldOakTree); }

                    if (gd.world[i].type == 13)
                    { itemcopy = Instantiate(OldOakBush); }

                    if (gd.world[i].type == 14)
                    { itemcopy = Instantiate(OldOaksmallbushes); }

                    if (gd.world[i].type == 15)
                    { itemcopy = Instantiate(grass3); }

                    if (gd.world[i].type == 16)
                    { itemcopy = Instantiate(poppy3); }

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

                if (gd.items[o].type == 10)
                { itemcopy = Instantiate(OldOakLog); }

                if (gd.items[o].type == 11)
                { itemcopy = Instantiate(OldOakleaves); }

                if (gd.items[o].type == 12)
                { itemcopy = Instantiate(AxeT2); }

                if (gd.items[o].type == 13)
                { itemcopy = Instantiate(wheelbarrowT2); }

                if (gd.items[o].type == 14)
                { itemcopy = Instantiate(wheelbarrowT3); }

                if (gd.items[o].type == 15)
                { itemcopy = Instantiate(OakFarmPlot); }

                if (gd.items[o].type == 16)
                { itemcopy = Instantiate(T1conveyorBelt); }

                if (gd.items[o].type == 17)
                { itemcopy = Instantiate(T1conveyorBeltRight); }

                if (gd.items[o].type == 18)
                { itemcopy = Instantiate(T1conveyorBeltLeft); }

                if (gd.items[o].type == 19)
                { itemcopy = Instantiate(T2conveyorBelt); }

                if (gd.items[o].type == 20)
                { itemcopy = Instantiate(T2conveyorBeltRight); }

                if (gd.items[o].type == 21)
                { itemcopy = Instantiate(T2conveyorBeltLeft); }

                if (gd.items[o].type == 22)
                { itemcopy = Instantiate(T2BerryFarmPlot); }

                /////////////////////////////////////////////////////////filesTypes
                if (gd.items[o].typeFolder == 0)
                { itemcopy.transform.parent = ItemsFolder.transform; }

                if (gd.items[o].typeFolder == 1)
                { itemcopy.transform.parent = ItemsFolder.transform; }

                if (gd.items[o].typeFolder == 2)
                { itemcopy.transform.parent = coinsobject.foldersell.transform; }

                if (gd.items[o].typeFolder == 3)
                { itemcopy.transform.parent = ItemsFolder.transform; }

                if (gd.items[o].type == 15 | gd.items[o].type == 22)
                {
                    itemcopy.transform.GetChild(0).eulerAngles = new Vector3(0, 0, gd.items[o].rotation);
                    itemcopy.GetComponent<ItemCreator>().On = gd.items[o].ItemCreatorOn;
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
