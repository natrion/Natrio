using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum chunktype : byte
//{
//    notype = 0,
//    Chunk = 1,
//    oak_tree = 2,
//    oak_bush = 3,
//    smallbushes = 4,
//    OakBiomGrass = 5
//}

[System.Serializable]
public class chunkdata
{
    public int Layer;
    public byte type;
    public float[] position;
    public float rotation;

    public chunkdata(Transform childItem ,  GameObject[] NatureThingsC)
    {
        for (byte i = 1; i < NatureThingsC.Length; i++)
        {
            if (NatureThingsC[i].CompareTag(childItem.gameObject.tag))
            {
                type = i;
            }
        }

        position = new float[2];
        position[0] = childItem.position.x;
        position[1] = childItem.position.y;
        rotation = childItem.eulerAngles.z;
        Layer = childItem.GetComponent<SpriteRenderer>().sortingOrder;
    }
}