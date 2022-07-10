using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PG : MonoBehaviour
{
    public float PerlinNoiseX;
    public float PerlinNoiseY;
    public float seed;
    public float biom_Diversity;
    void Start()
    {
        seed = Random.Range(1f, 400f);
    }
    void Update()
    {

        float PerlinNoise = Mathf.PerlinNoise((PerlinNoiseX/100)+seed, (PerlinNoiseY/100)+seed);
        //print(PerlinNoise);
    }
}
