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
    public byte type;
    public float[] position;
    public float rotation;

    public chunkdata(Transform childItem)
    {
        if (childItem.gameObject.CompareTag("Chunk"))
        { type = 1; }

        /////////////////////////////////////OakBiom
        if (childItem.gameObject.CompareTag("oak_tree"))
        { type = 2; }

        if (childItem.gameObject.CompareTag("oak_bush"))
        { type = 3; }

        if (childItem.gameObject.CompareTag("smallbushes"))
        { type = 4; }

        if (childItem.gameObject.CompareTag("OakBiomGrass"))
        { type = 5; }

        if (childItem.gameObject.CompareTag("OakBiomPoppy"))
        { type = 6; }

        /////////////////////////////////////BerryBiom
        if (childItem.gameObject.CompareTag("appletree"))
        { type = 7; }

        if (childItem.gameObject.CompareTag("blueberry_bush"))
        { type = 8; }

        if (childItem.gameObject.CompareTag("blue_berry_small_bushes"))
        { type = 9; }

        if (childItem.gameObject.CompareTag("BerryBiomGrass"))
        { type = 10; }

        if (childItem.gameObject.CompareTag("BerryBiomPoppy"))
        { type = 11; }


        position = new float[2];
        position[0] = childItem.position.x;
        position[1] = childItem.position.y;
        rotation = childItem.rotation.z;

    }
}