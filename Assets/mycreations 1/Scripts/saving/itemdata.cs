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

    public itemdata(Transform childItem, GameObject[] ItemC, bool[] ItemsThatCanCrerate, bool[] ItemsToNotRotate)
    {
        type = 0;
        for (byte i = 1; i < ItemC.Length-1; i++)
        {
            if (ItemC[i].CompareTag(childItem.gameObject.tag))
            {
                type = i;
            }
        }             

        if (childItem.gameObject.CompareTag("ItemSorter"))
        {
            text = childItem.GetChild(1).GetComponent<TextMesh>().text;
        }

        if (ItemsThatCanCrerate[type] == true)
        {
            ItemCreatorOn = childItem.GetComponent<ItemCreator>().On;
        }
        else
        {
            ItemCreatorOn = false;
        }

        if (ItemsToNotRotate[type] == true)
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