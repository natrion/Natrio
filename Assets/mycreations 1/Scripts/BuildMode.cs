using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : MonoBehaviour
{
    [SerializeField] private NetworkComunicator NetworkComunicator;
    public Transform itemfolder;
    public Transform playerFolder;
    public int WhatColiderIsSolid = -1;
    public bool Rotateble;   
    public bool isInbildingMode;
    public bool isFloor;
    public bool isItemConvertor;

    public string[] ConvertingItemTag;
    public float[] TimeToConvert;
    public int[] CreatingFromConvertingItemNumber;

    public bool itemSorter;
    public bool isConveyorBelt;
    public bool isTurnConveyorBelt;
    public float ConveyorSpeed;
    private float x;
    private float y;
    private Vector3 PositionOnStrat;
    private Animator m_Animator;
    public bool CreateItemIERunning;

    [System.Serializable]
    public struct Recepy
    {
        public string[] ConvertingItemTag ;
        public int[] howManyItems;

        public int[] howManyItemsThere ;

        public float TimeToConvert;
        public GameObject[] CreatingFromConvertingItem;
        
        

        
    }

    public Recepy[] Recepys;

    public void CreateItem(int whatRecepy)
    {
        if (CreateItemIERunning == false)
        {
            CreateItemIERunning = true;
            whatRecepyOn = whatRecepy;
            StartCoroutine(CreateItemIE());
        }
    }
    private int whatRecepyOn;
    IEnumerator CreateItemIE()
    {
        bool createItem = true;
        for (int i = 0; i < Recepys[whatRecepyOn].howManyItems.Length; i++)
        {
            if (Recepys[whatRecepyOn].howManyItems[i] > Recepys[whatRecepyOn].howManyItemsThere[i])
            {
                createItem = false;
            }
        }
        
        if (createItem == true)
        {
            yield return new WaitForSeconds(Recepys[whatRecepyOn].TimeToConvert);

            for (int n = 0; n < Recepys[whatRecepyOn].CreatingFromConvertingItem.Length; n++)
            {
                GameObject ItemCopy = Instantiate(Recepys[whatRecepyOn].CreatingFromConvertingItem[n]);
                ItemCopy.transform.position = transform.position - transform.up * 0.4f;
                ItemCopy.transform.parent = itemfolder;
            }

            for (int i = 0; i < Recepys[whatRecepyOn].howManyItems.Length; i++)
            {
                Recepys[whatRecepyOn].howManyItemsThere[i] = 0;
            }
        }
        CreateItemIERunning = false;
    }

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) )
        {
            if(itemSorter == true)
            {

                if (playerFolder.childCount == 1)
                {
                    transform.GetChild(1).GetComponent<TextMesh>().text = "Sorting " + playerFolder.GetChild(0).gameObject.tag;
                }
                else
                {
                    transform.GetChild(1).GetComponent<TextMesh>().text = "Not Sorting";
                }    
                
            }          
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(ItemCreator.BuildModeIsRunning == false)
            {
                //BoxCollider2D[] BoxColliders = transform.GedtComponents<BoxCollider2D>();

                //BoxColliders[1].isTrigger = true;
                //BoxColliders[2].isTrigger = true;

                m_Animator.SetBool("Select", true);
                ItemCreator.BuildModeIsRunning = true;
                isInbildingMode = true;
                StartCoroutine(BuildModeFunction());
                
            }
        }
    }

    IEnumerator BuildModeFunction()
    {
        NetworkComunicator.SentInformation(false, false, true, 0, transform);
        if(WhatColiderIsSolid > -1 )
        {
            Collider2D[] Colliders = transform.GetComponents<Collider2D>();
            Colliders[WhatColiderIsSolid].isTrigger = true;
        }

        PositionOnStrat = transform.position;

        yield return new WaitForSeconds(0.01f);

        while (!Input.GetMouseButtonDown(0))
        {
            Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            x = MousePosition.x / 0.5f;
            y = MousePosition.y / 0.5f;

            x = Mathf.Round(x) * 0.5f;
            y = Mathf.Round(y) * 0.5f;

            Vector2 xY = new Vector2(x, y);
            RaycastHit2D hitXy = Physics2D.Raycast(xY, Vector2.zero);

           if (hitXy.collider != null)
           {
                if(hitXy.collider.GetComponent<BuildMode>() !=null & hitXy.collider.transform != transform & isFloor == false)
                {
                    if (hitXy.collider.GetComponent<BuildMode>().isFloor == true)
                    { m_Animator.SetBool("SelectRed", false); }
                    else if (hitXy.collider.transform.position != transform.position & hitXy.collider.transform.parent != transform)
                    { m_Animator.SetBool("SelectRed", true); }
                }
                else if (hitXy.collider.transform.position != transform.position & hitXy.collider.transform.parent != transform)
                {
                    if (hitXy.collider.GetComponent<SpriteRenderer>()!=null)
                    {
                        if (hitXy.collider.GetComponent<SpriteRenderer>().sortingLayerName == "nature0")
                        {
                            { m_Animator.SetBool("SelectRed", false); }
                        }
                        else
                        {
                            { m_Animator.SetBool("SelectRed", true); }
                        }
                    }
                    
                }
           }
           else { m_Animator.SetBool("SelectRed", false); }

            transform.position = new Vector3(x, y, 0);

            yield return new WaitForSeconds(0.0001f);

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (Rotateble == true)
                {
                    transform.eulerAngles -= Vector3.forward * 90; 
                }
                else
                {
                    transform.GetChild(0).eulerAngles -= Vector3.forward * 90; ;
                } 
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);

        yield return new WaitForSeconds(0.0001f);

        Vector2 BuildModeAutput = new Vector2(x, y);
        RaycastHit2D hit = Physics2D.Raycast(BuildModeAutput, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<BuildMode>() != null & hit.collider.transform != transform & isFloor == false)
            {
                if (hit.collider.GetComponent<BuildMode>().isFloor == true  )
                { transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0); }
                else
                {
                    transform.position = PositionOnStrat;
                }
            }
            else 
            {
                if (hit.collider.GetComponent<SpriteRenderer>().sortingLayerName == "nature0")
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
                }
                else
                {
                    transform.position = PositionOnStrat;
                }
            }
        }
        else
        { transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0); }

        //BoxCollider2D[] BoxColliders2 = transform.GetComponents<BoxCollider2D>();

        ItemCreator.BuildModeIsRunning = false;
        
        //BoxColliders2[1].isTrigger = false;
        //BoxColliders2[2].isTrigger = false;

        m_Animator.SetBool("SelectRed", false);
        m_Animator.SetBool("Select", false);

        if (WhatColiderIsSolid > -1)
        {
            Collider2D[] Colliders = transform.GetComponents<Collider2D>();
            Colliders[WhatColiderIsSolid].isTrigger = false;
        }
        yield return new WaitForSeconds(0.1f);
        isInbildingMode = false;
        NetworkComunicator.SentInformation(false, true, false, 0, transform);
    }
}  
