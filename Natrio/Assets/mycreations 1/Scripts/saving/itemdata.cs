using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class itemdata
{
    public string text;
    public bool ItemCreatorOn;
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

        if (childItem.gameObject.CompareTag("OakFarmPlot"))
        { type = 15; }
        if (childItem.gameObject.CompareTag("T1conveyorBelt"))
        { type = 16; }

        if (childItem.gameObject.CompareTag("T1conveyorBeltRight"))
        { type = 17; }
        if (childItem.gameObject.CompareTag("T1conveyorBeltLeft"))
        { type = 18; }

        if (childItem.gameObject.CompareTag("T2conveyorBelt"))
        { type = 19; }
        if (childItem.gameObject.CompareTag("T2conveyorBeltRight"))
        { type = 20; }
        if (childItem.gameObject.CompareTag("T2conveyorBeltLeft"))
        { type = 21; }
        if (childItem.gameObject.CompareTag("T2BerryFarmPlot"))
        { type = 22; }

        if (childItem.gameObject.CompareTag("ItemSorter"))
        { type = 23; }

        if (childItem.gameObject.CompareTag("AutomatickSaw"))
        { type = 24; }
        if (childItem.gameObject.CompareTag("oakPlanks"))
        { type = 25; }

        if (childItem.gameObject.CompareTag("T2AutomatickSaw"))
        { type = 26; }
        if (childItem.gameObject.CompareTag("BerryPlanks"))
        { type = 27; }

        typeFolder = 0;

        if (childItem.parent.gameObject.name == "items")
        { typeFolder = 1; }

        if (childItem.parent.gameObject.name == "thingsforsell")
        { typeFolder = 2; }

        if (childItem.parent.gameObject.name == "playerfolder")
        { typeFolder = 3; }

        if (childItem.gameObject.CompareTag("ItemSorter"))
        {
            text = childItem.GetChild(1).GetComponent<TextMesh>().text;
        }

        if (childItem.gameObject.CompareTag("OakFarmPlot") | childItem.gameObject.CompareTag("T2BerryFarmPlot"))
        {
            ItemCreatorOn = childItem.GetComponent<ItemCreator>().On;
        }
        else
        {
            ItemCreatorOn = false;
        }

        if (childItem.gameObject.CompareTag("OakFarmPlot") | childItem.gameObject.CompareTag("T2BerryFarmPlot") | childItem.gameObject.CompareTag("ItemSorter"))
        {
            rotation = childItem.GetChild(0).eulerAngles.z;           
        }
        else
        {
            rotation = childItem.eulerAngles.z;           
        }

        position = new float[2];
        position[0] = childItem.position.x;
        position[1] = childItem.position.y;
    }
}