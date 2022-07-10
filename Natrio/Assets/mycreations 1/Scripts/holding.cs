using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holding : MonoBehaviour
{
    public int what_damage;
    private bool cutingpisible;
    private float distanceafter;
    private Transform holdedthing;
    private bool pick;
    private bool calldawn;
    private int how_much_cost;
    private GameObject folderforsale;
    public GameObject flolderitems;
   // private Animator player_Animator;
    private GameObject item;
    private Animator item_Animator;
    void Start()
    {
        //player_Animator = gameObject.GetComponent<Animator>();
        pick = false;
        calldawn = false;       
        what_damage = 1;

    }

    IEnumerator clickanimation()
    {
        yield return new WaitForSeconds(0.5f);
        item_Animator.SetBool("cutting", false);
        holdedthing.eulerAngles = transform.eulerAngles - new Vector3(0, 0, 180f);
        holdedthing.position = transform.position;
        holdedthing.position -= holdedthing.up * distanceafter;        
    }

    IEnumerator calldawnfunction()
    {
        yield return new WaitForSeconds(0.00001f);
        calldawn = false;
    }

    public void itemholding(float distance, float normalrotation, Transform holdingobject ,int how_much_costing, bool cutingpisibleinfunction, GameObject itemfunctiion, int damage, float PickUpRadius)
    {

        
        distanceafter = distance;
        cutingpisible = cutingpisibleinfunction;
        item = itemfunctiion;
        how_much_cost = how_much_costing;
        holdedthing = holdingobject;
        item_Animator = item.GetComponent<Animator>();

        if (pick == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (transform.position.y - holdedthing.position.y < PickUpRadius & transform.position.y - holdedthing.position.y > PickUpRadius * -1 & transform.position.x - holdedthing.position.x > PickUpRadius * -1 & transform.position.x - holdedthing.position.x < PickUpRadius)
                {
                    
                    pick = true;
                    calldawn = true;
                    StartCoroutine(calldawnfunction());
                    //player_Animator.SetBool("holding", true);
                    //FindObjectOfType<movement>().changespeed(1f);
                    holdedthing.parent = transform;
                    holdedthing.eulerAngles = transform.eulerAngles - new Vector3(0, 0, normalrotation);
                    holdedthing.position = transform.position;

                    holdedthing.position += transform.up * distance;
                    what_damage = damage;

                    FindObjectOfType<sellthings>().addcoinspotencial();
                }
            }
        }   
    }
    void Update()
    {

        if (pick == true)
        {
            if (Input.GetMouseButtonDown(0))
            {               
                if (cutingpisible == true)
                {
                    item_Animator.SetBool("cutting", true);
                    StartCoroutine(clickanimation());
                }

            }

            if (calldawn == false)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    what_damage = 1;
                    

                   // player_Animator.SetBool("holding", false);
                    
                    FindObjectOfType<movement>().changespeed(2f);
                    if (holdedthing.position.y < 1.4f & holdedthing.position.y > 0.4f & holdedthing.position.x > -1.4f & holdedthing.position.x < -0.4f)
                    {
                        folderforsale = GameObject.Find("thingsforsell");
                        holdedthing.parent = folderforsale.transform;                                                                            
                        FindObjectOfType<sellthings>().addcoinspotencial();                                                                                               
                    }
                    else
                    {
                        holdedthing.parent = flolderitems.transform;
                        what_damage = 1;
                        FindObjectOfType<sellthings>().addcoinspotencial();
                    }
                    pick = false;
                }
            }
        }

    }
}