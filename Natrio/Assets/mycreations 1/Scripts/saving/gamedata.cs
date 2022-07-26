using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class gamedata
{
    public int playerCoins;
    public float[] playerPosition;
    public float seed;

    public gamedata(smalldata smalldata)
    {
        seed = smalldata.seedobject.seed;
        playerCoins = smalldata.coinsobject.how_many_coins;
        playerPosition = new float[2];
        playerPosition[0] = smalldata.player.position.x;
        playerPosition[1] = smalldata.player.position.y;
    }
}
