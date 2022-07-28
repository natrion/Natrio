using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smalldata : MonoBehaviour
{
    public GameObject Axe;
    public GameObject wheelbarrow;
    public GameObject ItemsFolder;
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
        SL.saveGame(new gamedata(this));
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

        
        bool done = false;
        
        while (done == false)
        {
            OnItem ++;
            if (gd.items[OnItem] == null)
            {
                done = true;
            }
            else
            {
                if (gd.items[OnItem].type != 0)
                {
                    if (gd.items[OnItem].type == 1)
                    { itemcopy = Instantiate(Axe); }

                    if (gd.items[OnItem].type == 2)
                    { itemcopy = Instantiate(wheelbarrow); }

                    itemcopy.transform.position = new Vector3(gd.items[OnItem].position[0], gd.items[OnItem].position[1], 0);
                    itemcopy.transform.rotation = Quaternion.Euler(0, 0, gd.items[OnItem].rotation);
                }
                 
            }  
        }           
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
    }
}
