using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PG : MonoBehaviour
{
    public Biom[] Bioms;

    public GameObject copyChunk;

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

    [System.Serializable]

    public struct Deposit
    {
        public int Rarety;
        public GameObject DepositGameObject;
    }
    [System.Serializable]
    public struct NatureObject
    {
        public float Comoness;
        public GameObject NatureGameObject;
        public int SpawningLayerNatureObject;
    }
    [System.Serializable]
    public struct Biom
    {        
        public NatureObject[] NatureObjects;
        public Deposit[] Deposits;
        public Vector2 biomFromTo;
    }
    

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

                        int random = Random.Range(0, 3);


                        for (int c = 0; c < Bioms.Length; c++)//All bioms
                        {
                            if (Bioms[c].biomFromTo.x <PerlinNoise & Bioms[c].biomFromTo.y > PerlinNoise)//Biom
                            {
                                for (int a = 0; a < Bioms[c].NatureObjects.Length; a++)//All NatureThings
                                {
                                    for (float o = 0; o < PerlinNoise * vegetationNumber * Bioms[c].NatureObjects[a].Comoness; o++)//NatureThing
                                    {
                                        GameObject CopyNatureGameObject = Instantiate(Bioms[c].NatureObjects[a].NatureGameObject);
                                        if (Bioms[c].NatureObjects[a].SpawningLayerNatureObject == -1)                                       
                                            spawning(CopyNatureGameObject, chunk);                                        
                                        else                                        
                                            spawningLayer(CopyNatureGameObject, chunk, Bioms[c].NatureObjects[a].SpawningLayerNatureObject);                                                                                
                                    }                                    
                                }
                                for (int e = 0; e < Bioms[c].Deposits.Length; e++)//All Deposits
                                {
                                    if (Random.Range(0, Bioms[c].Deposits[e].Rarety) == 1)
                                    {
                                        GameObject DepositCopy = Instantiate(Bioms[c].Deposits[e].DepositGameObject);
                                        spawning(DepositCopy, chunk);
                                    }                                                          
                                }
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


 