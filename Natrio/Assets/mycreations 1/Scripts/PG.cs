using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PG : MonoBehaviour
{

    
    public GameObject copyChunk;

    public GameObject oakTree;
    public GameObject berryTree;
    public GameObject OldOakTree;

    public GameObject oakBush;
    public GameObject berryBush;
    public GameObject OldOakBush;

    public GameObject oakSmallBushes;
    public GameObject berrySmallBushes;
    public GameObject OldOakSmallBushes;

    public GameObject grass1;
    public GameObject grass2;
    public GameObject grass3;

    public GameObject poppy1;
    public GameObject poppy2;
    public GameObject poppy3;

    public GameObject Stone;

    public float  vegetationNumber;
    public float size;
    public float seed;
    public float biom_Diversity;

    private float x;
    private float y;
    public Transform playerTransfom;
    private bool spawn;
    private float PerlinNoise;
    private bool done;
    public smalldata smalldata;
    private float seedchanger;

    public void StartChunk()
    {
        spawn = true;
        seed = Random.Range(1f, 4000000000f);
        done = false;
        bool seedexist = float.IsNaN(smalldata.seedOfLastGame);
        if (seedexist == false)
        {
            seed = smalldata.seedOfLastGame;
        }
        else
        {            
            while (done == true)
            {
                if(PerlinNoise < 0.3f)
                {
                    seedchanger = 4.444f;
                }else
                {
                    seedchanger = 111.111f;
                }
                seed += seedchanger;
                PerlinNoise = Mathf.PerlinNoise((size / biom_Diversity) + seed, (size / biom_Diversity) + seed);
                if (PerlinNoise < 0.1)
                {
                    done = true;
                }
            }
        }
        StartCoroutine(chunkgenerate());
      
    }
    IEnumerator chunkgenerate()
    {
        
        for (int i = 1; i > 0; i++)
        {
            bool seednotexist = float.IsNaN(smalldata.seedOfLastGame);
            if (seednotexist == false)
            {
                if (smalldata.seedOfLastGame != seed)
                {
                    seed = smalldata.seedOfLastGame;
                }
            }

            float x_ = playerTransfom.position.x / 5;
            float y_ = playerTransfom.position.y / 5;
            x = Mathf.Round(x_ * 1) ;
            y = Mathf.Round(y_ * 1) ;
                        
            for (float X = x-1; X < x+2;X++)
            {
                for (float Y = y - 1; Y < y+2; Y++)
                {
                    int childs = transform.childCount;
                    for (int onchild = 0; onchild < childs; onchild++)
                    {
                        Transform child = transform.GetChild(onchild);
                        if (child.position == new Vector3(X * 5, Y * 5, 0))
                        {
                            spawn = false;
                        }
                    }
                    
                   
                   // RaycastHit2D hit = Physics2D.Raycast(new Vector2(X * 5, Y * 5), Vector2.zero);

                   //if (hit.collider != null)
                   //{
                   //spawn = false;
                   // }

                    if  (spawn == true)
                    {
                        GameObject chunk = Instantiate(copyChunk);
                        chunk.transform.position = new Vector3(X * 5, Y * 5, 0);
                        chunk.transform.parent = transform;
                        
                        
                        PerlinNoise = Mathf.PerlinNoise((X*size / biom_Diversity) + seed, (Y*size / biom_Diversity) + seed);

                        ////////////////////////////////////////////////////oak_biom
                        if (PerlinNoise < 0.5)
                        {
                            
                            for (float o = 0; o<  PerlinNoise * vegetationNumber;o++)
                            {
                                 GameObject OakTreeCopy = Instantiate(oakTree);
                                 spawning(OakTreeCopy, chunk);
                            }
                            for (float a = 0; a < PerlinNoise * vegetationNumber*2; a++)
                            {
                                 GameObject OakBushCopy = Instantiate(oakBush);
                                 spawning(OakBushCopy, chunk);
                            }
                            for (float s = 0; s < PerlinNoise * vegetationNumber*3; s++)
                            {
                                 GameObject OakBushesCopy = Instantiate(oakSmallBushes);
                                 spawning(OakBushesCopy, chunk);
                            }
                            for (float h = 0; h < PerlinNoise * vegetationNumber * 5; h++)
                            {
                                GameObject grass1Copy = Instantiate(grass1);
                                spawning(grass1Copy, chunk);
                            }
                            for (float j = 0; j < PerlinNoise * vegetationNumber * 4; j++)
                            {
                                GameObject poppy1Copy = Instantiate(poppy1);
                                spawning(poppy1Copy, chunk);
                            }
                        }
                        ////////////////////////////////////////////////////Old_oak_biom
                        if (PerlinNoise > 0.5 & PerlinNoise < 0.7)
                        {
                            for (float o1 = 0; o1 < PerlinNoise * vegetationNumber; o1++)
                            {
                                GameObject OakTreeCopy = Instantiate(OldOakTree);
                                spawningLayer(OakTreeCopy, chunk,2);
                            }
                            for (float a1 = 0; a1 < PerlinNoise * vegetationNumber * 2; a1++)
                            {
                                GameObject OakBushCopy = Instantiate(OldOakBush);
                                spawningLayer(OakBushCopy, chunk,1);
                            }
                            for (float s1 = 0; s1 < PerlinNoise * vegetationNumber * 3; s1++)
                            {
                                GameObject OakBushesCopy = Instantiate(OldOakSmallBushes);
                                spawningLayer(OakBushesCopy, chunk,0);
                            }
                            for (float h1 = 0; h1 < PerlinNoise * vegetationNumber * 5; h1++)
                            {
                                GameObject grass1Copy = Instantiate(grass3);
                                spawning(grass1Copy, chunk);
                            }
                            for (float j1 = 0; j1 < PerlinNoise * vegetationNumber * 4; j1++)
                            {
                                GameObject poppy1Copy = Instantiate(poppy3);
                                spawning(poppy1Copy, chunk);
                            }
                            int random = Random.Range(0, 3);
                            if (random ==  2)
                            {
                                GameObject StoneCopy = Instantiate(Stone);
                                spawning(StoneCopy, chunk);
                            }
                        }
                        ////////////////////////////////////////////////////berry_biom
                        if (PerlinNoise > 0.7)
                        {
                            for (float d = 0; d < PerlinNoise * vegetationNumber; d++)
                            {
                                GameObject OakTreeCopy = Instantiate(berryTree);
                                spawning(OakTreeCopy, chunk);
                            }
                            for (float f = 0; f < PerlinNoise * vegetationNumber * 2; f++)
                            {
                                GameObject OakBushCopy = Instantiate(berryBush);
                                spawning(OakBushCopy, chunk);
                            }
                            for (float g = 0; g < PerlinNoise * vegetationNumber * 3; g++)
                            {
                                GameObject OakBushesCopy = Instantiate(berrySmallBushes);
                                spawning(OakBushesCopy, chunk);
                            }
                            for (float h = 0; h < PerlinNoise * vegetationNumber * 5; h++)
                            {
                                GameObject grass2Copy = Instantiate(grass2);
                                spawning(grass2Copy, chunk);
                            }
                            for (float j = 0; j < PerlinNoise * vegetationNumber * 4; j++)
                            {
                                GameObject poppy2Copy = Instantiate(poppy2);
                                spawning(poppy2Copy, chunk);
                            }
                        }
                    }
                    spawn = true;
                }
            }
            yield return new WaitForSeconds(1);
        }

    }
    private void spawning(GameObject Copy,GameObject chunkCopy)
    {
        Copy.transform.position = new Vector3(Mathf.Round(chunkCopy.transform.position.x + Random.Range(-2.5f, 2.5f) * 1) , Mathf.Round(chunkCopy.transform.position.y + Random.Range(-2.5f, 2.5f) * 1), 0);
        Copy.transform.parent = transform;
    }
    private void spawningLayer(GameObject Copy, GameObject chunkCopy, int Layer)
    {
        Copy.transform.position = new Vector3(Mathf.Round(chunkCopy.transform.position.x + Random.Range(-2.5f, 2.5f) * 1), Mathf.Round(chunkCopy.transform.position.y + Random.Range(-2.5f, 2.5f) * 1), 0);
        Copy.transform.parent = transform;
        int yChunkInt = Mathf.RoundToInt(chunkCopy.transform.position.y);
        int YthingInt = Mathf.RoundToInt(Copy.transform.position.y);
        int ipositionInChunk = 5-(YthingInt - (yChunkInt - 2) );
        Copy.GetComponent<SpriteRenderer>().sortingOrder = (ipositionInChunk*2 ) +Layer;
    }

}


 