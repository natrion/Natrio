using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : MonoBehaviour
{
    public Transform playerFolder;
    public int WhatColiderIsSolid = -1;
    public bool Rotateble;
    public bool itemSorter;
    public bool isInbildingMode;
    public bool isConveyorBelt;
    public bool isTurnConveyorBelt;
    public float ConveyorSpeed;
    private float x;
    private float y;
    private Vector3 PositionOnStrat;
    private Animator m_Animator;

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
        if(WhatColiderIsSolid > -1 )
        {
            PolygonCollider2D[] PolygonColliders = transform.GetComponents<PolygonCollider2D>();
            PolygonColliders[WhatColiderIsSolid].isTrigger = true;
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
                if (hitXy.collider.transform.position != transform.position & hitXy.collider.transform.parent !=transform) 
                { m_Animator.SetBool("SelectRed", true); }
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

        yield return new WaitForSeconds(0.01f);

        Vector2 BuildModeAutput = new Vector2(x, y);
        RaycastHit2D hit = Physics2D.Raycast(BuildModeAutput, Vector2.zero);

        if (hit.collider != null)
        {
            transform.position = PositionOnStrat;
        }
        else
        { transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0); }

        //BoxCollider2D[] BoxColliders2 = transform.GetComponents<BoxCollider2D>();

        ItemCreator.BuildModeIsRunning = false;
        isInbildingMode = false;
        //BoxColliders2[1].isTrigger = false;
        //BoxColliders2[2].isTrigger = false;

        m_Animator.SetBool("SelectRed", false);
        m_Animator.SetBool("Select", false);

        if (WhatColiderIsSolid > -1)
        {
            PolygonCollider2D[] PolygonColliders = transform.GetComponents<PolygonCollider2D>();
            PolygonColliders[WhatColiderIsSolid].isTrigger = false;
        }
    }
}  
