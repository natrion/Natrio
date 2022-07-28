using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class itemdata
{
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
        position = new float[2];
        position[0] = childItem.position.x;
        position[1] = childItem.position.y;
        rotation = childItem.rotation.z;
    }
}