using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class gamedata
{
    public int playerCoins;
    public float[] playerPosition;
    public float seed;
    public itemdata[] items;

    public gamedata(smalldata smalldata , sellthings sellthings)
    {
        seed = smalldata.seedobject.seed;
        playerCoins = smalldata.coinsobject.how_many_coins;
        playerPosition = new float[2];
        playerPosition[0] = smalldata.player.position.x;
        playerPosition[1] = smalldata.player.position.y;

        int itemObjectnumber = 0;

        int SellFolderChildCount = sellthings.foldersell.transform.childCount;
        int ItemsFolderChildCount = smalldata.ItemsFolder.transform.childCount;
        int PlayerFolderChildCount = smalldata.PlayerFolder.transform.childCount;

        items = new itemdata[ItemsFolderChildCount + PlayerFolderChildCount+ SellFolderChildCount+1];

        for (int i = 0; i < ItemsFolderChildCount; i++)
        {
            itemObjectnumber ++;
            //if(!smalldata.ItemsFolder.transform.GetChild(i).CompareTag("Untagged"))
            //{
                items[itemObjectnumber] = new itemdata(smalldata.ItemsFolder.transform.GetChild(i));
            //}           
        }

        

        for (int o = 0; o < PlayerFolderChildCount; o++)
        {
            itemObjectnumber++;
            //if(!smalldata.ItemsFolder.transform.GetChild(i).CompareTag("Untagged"))
            //{
            items[itemObjectnumber] = new itemdata(smalldata.PlayerFolder.transform.GetChild(o));
            //}           
        }

        

        for (int p = 0; p < SellFolderChildCount; p++)
        {
            itemObjectnumber++;
            //if(!smalldata.ItemsFolder.transform.GetChild(i).CompareTag("Untagged"))
            //{
            items[itemObjectnumber] = new itemdata(sellthings.foldersell.transform.GetChild(p));
            //}           
        }
    }
}
