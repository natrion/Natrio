using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PG : MonoBehaviour
{

    public float seed;
    public float biom_Diversity;
    private GameObject tree;
    public GameObject copyChunk;
    private float x;
    private float y;
    public Transform playerTransfom;
    void Start()
    {
        
        seed = Random.Range(1f, 400f);
        StartCoroutine(chunkgenerate());

    }
    IEnumerator chunkgenerate()
    {
        for (int i = 0; i < 10000; i++)
        {
            float x_ = playerTransfom.position.x / 5;
            float y_ = playerTransfom.position.y / 5;
            x = Mathf.Round(x_ * 1) ;
            y = Mathf.Round(y_ * 1) ;
            
            print(x);
            print(y);

            seed = Random.Range(1f, 400f);
            for (float X = x-2; X < x+2;X++)
            {
                for (float Y = y - 2; Y < y+2; Y++)
                {

                    GameObject chunk = Instantiate(copyChunk);
                    chunk.transform.position = new Vector3(X*5, Y*5, 0);
                    chunk.transform.parent = transform;
                    float PerlinNoise = Mathf.PerlinNoise((x / 10) + seed, (y / 10) + seed);
                    

                }
            }
            yield return new WaitForSeconds(1);

        }
        
    }

    void Update()
    {
        
    }  
}


