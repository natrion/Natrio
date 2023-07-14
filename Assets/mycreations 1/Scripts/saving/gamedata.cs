using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class gamedata
{
    public bool autoseling;
    public int playerCoins;
    public float[] playerPosition;
    public float seed;
    public itemdata[] items;
    public chunkdata[] world;

    public gamedata(smalldata smalldata , sellthings sellthings)
    {
        autoseling = sellthings.autoseling;
        seed = smalldata.seedobject.seed;
        playerCoins = smalldata.coinsobject.how_many_coins;
        playerPosition = new float[2];
        playerPosition[0] = smalldata.player.position.x;
        playerPosition[1] = smalldata.player.position.y;

        smalldata.Folders[2] = sellthings.foldersell.transform;

        

        int itemslength = 0;
        for (int n = 1; n < smalldata.Folders.Length; n++)
        {
            itemslength += smalldata.Folders[n].childCount;
        }
        items = new itemdata[itemslength+1];

        byte i;
        int itemObjectnumber = 0;
        for ( i = 1; i <  smalldata.Folders.Length; i++)
        {
            for (int d = 0; d < smalldata.Folders[i].childCount; d++)
            {
                
                //if(!smalldata.ItemsFolder.transform.GetChild(i).CompareTag("Untagged"))
                //{
                items[itemObjectnumber] = new itemdata(smalldata.Folders[i].transform.GetChild(d), smalldata.Items);
                items[itemObjectnumber].typeFolder = i ;
                itemObjectnumber++;
            }
        }        

        int GrassFolderChildCount = smalldata.GrassFolder.transform.childCount;
        world = new chunkdata[GrassFolderChildCount];
        for (int a = 0; a < GrassFolderChildCount; a++)
        {            
            //if(!smalldata.ItemsFolder.transform.GetChild(i).CompareTag("Untagged"))
            //{
            world[a] = new chunkdata(smalldata.GrassFolder.transform.GetChild(a) , smalldata.Nature);
            //}           
        }
    }
}
