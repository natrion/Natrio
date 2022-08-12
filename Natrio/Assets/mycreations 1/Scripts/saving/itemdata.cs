using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class itemdata
{
    public byte typeFolder;
    public byte type;
    public float[] position;
    public float rotation;

    public itemdata(Transform childItem)
    {
        type = 0;
        if (childItem.gameObject.CompareTag("Axe"))
        { type = 1; }
        if (childItem.gameObject.CompareTag("wheelbarrow"))
        { type = 2; }

        if (childItem.gameObject.CompareTag("OakLog"))
        { type = 3; }
        if (childItem.gameObject.CompareTag("berryLog"))
        { type = 4; }

        if (childItem.gameObject.CompareTag("Leaves"))
        { type = 5; }
        if (childItem.gameObject.CompareTag("BerryLeaves"))
        { type = 6; }

        if (childItem.gameObject.CompareTag("Apple"))
        { type = 7; }
        if (childItem.gameObject.CompareTag("PurpleBerry"))
        { type = 8; }
        if (childItem.gameObject.CompareTag("BlueBerry"))
        { type = 9; }
        if (childItem.gameObject.CompareTag("OldOakLog"))
        { type = 10; }
        if (childItem.gameObject.CompareTag("OldOakleaves"))
        { type = 11; }

        if (childItem.gameObject.CompareTag("AxeT2"))
        { type = 12; }
        if (childItem.gameObject.CompareTag("wheelbarrowT2"))
        { type = 13; }
        if (childItem.gameObject.CompareTag("wheelbarrowT3"))
        { type = 14; }

        typeFolder = 0;

        if (childItem.parent.gameObject.name == "items")
        { typeFolder = 1; }

        if (childItem.parent.gameObject.name == "thingsforsell")
        { typeFolder = 2; }

        if (childItem.parent.gameObject.name == "playerfolder")
        { typeFolder = 3; }

        position = new float[2];
        position[0] = childItem.position.x;
        position[1] = childItem.position.y;
        rotation = childItem.eulerAngles.z;

    }
}