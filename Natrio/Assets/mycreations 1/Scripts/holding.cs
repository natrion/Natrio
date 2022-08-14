using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holding : MonoBehaviour
{
    public bool isMouseOver;
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

    private AudioSource[] Audio;
    private AudioSource GrabAudio;
    private AudioSource DropAudio;

    void Start()
    {
        Audio = flolderitems.GetComponents<AudioSource>();
        GrabAudio = Audio[0];
        DropAudio = Audio[1];
        //player_Animator = gameObject.GetComponent<Animator>();
        pick = false;
        calldawn = false;       
        what_damage = 1;

    }

    IEnumerator clickanimation()
    {
        item_Animator.SetBool("cutting", true);
        yield return new WaitForSeconds(0.40f);
        item_Animator.SetBool("cutting", false);
        holdedthing.eulerAngles = transform.eulerAngles;
        holdedthing.position = transform.position;
        holdedthing.position += holdedthing.up * distanceafter;        
    }

    IEnumerator calldawnfunction()
    {
        yield return new WaitForSeconds(0.00001f);
        calldawn = false;
    }

    public void itemholding(float distance, float normalrotation, Transform holdingobject ,int how_much_costing, bool cutingpisibleinfunction, GameObject itemfunctiion, int damage, float PickUpRadius, bool xRotation, bool yRotation)
    {
        
        

        if (pick == false  )
        {
            
            
                if (transform.position.y - holdingobject.position.y < PickUpRadius & transform.position.y - holdingobject.position.y > PickUpRadius * -1 & transform.position.x - holdingobject.position.x > PickUpRadius * -1 & transform.position.x - holdingobject.position.x < PickUpRadius)
                {
                   
                    if(holdingobject.GetComponent<Rigidbody2D>() != null )
                    {
                        holdingobject.GetComponent<Rigidbody2D>().simulated = false;
                    }
                    GrabAudio.Play();
                    pick = true;
                    distanceafter = distance;
                    cutingpisible = cutingpisibleinfunction;
                    item = itemfunctiion;
                    how_much_cost = how_much_costing;
                    holdedthing = holdingobject;
                    item_Animator = item.GetComponent<Animator>();

                    holdedthing.localScale = new Vector3(1, 1, 0);
                    holdedthing.eulerAngles = transform.eulerAngles - new Vector3(0, 0, normalrotation);

                    if (transform.parent.localScale == new Vector3(1, 1, 0) | transform.eulerAngles == new Vector3(0, 0, 180))
                    {
                        if (xRotation ==true)
                        {
                            holdedthing.localScale = new Vector3(-1, holdedthing.localScale.y, 0);
                        }
                        if (yRotation == true)
                        {
                            holdedthing.localScale = new Vector3(holdedthing.localScale.x,-1 , 0);
                        }
                    }
                    else
                    {
                        if(transform.parent.localScale == new Vector3(1, 1, 0) & transform.eulerAngles == new Vector3(0, 0, 0))
                        {
                            if (xRotation == true)
                            {
                                holdedthing.localScale = new Vector3(-1, holdedthing.localScale.y, 0);
                            }
                            if (yRotation == true)
                            {
                                holdedthing.localScale = new Vector3(holdedthing.localScale.x, -1, 0);
                            }
                        }
                        else
                        {
                            holdedthing.localScale = new Vector3(1, 1, 0);
                        }
                        
                    }
                    pick = true;
                    calldawn = true;
                    StartCoroutine(calldawnfunction());
                    //player_Animator.SetBool("holding", true);
                    //FindObjectOfType<movement>().changespeed(1f);
                    holdedthing.parent = transform;

                    holdedthing.position = transform.position;

                    holdedthing.position += transform.up * distance;
                    what_damage = damage;

                    FindObjectOfType<sellthings>().addcoinspotencial();
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
                    StartCoroutine(clickanimation());
                }
                if (holdedthing.GetComponent<item>().isItForTransportingItems == true & holdedthing.childCount != 0)
                {
                    Transform itemInWheelbarrow = holdedthing.GetChild(0);
                    itemInWheelbarrow.parent = flolderitems.transform;
                    itemInWheelbarrow.position += transform.up * 0.3f;

                    if (itemInWheelbarrow.GetComponent<Rigidbody2D>() != null)
                    {
                        itemInWheelbarrow.GetComponent<Rigidbody2D>().simulated = true;
                    }
                }
            }

            if (calldawn == false)
            {
                if (Input.GetMouseButtonDown(1) )
                {
                    DropAudio.Play();
                    if (holdedthing.GetComponent<item>().isItForTransportingItems == true & isMouseOver == true)
                    {
                        pick = true;
                        return;
                    }
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

                    if (holdedthing.GetComponent<Rigidbody2D>() != null)
                    {
                        holdedthing.GetComponent<Rigidbody2D>().simulated = true;
                    }
                    pick = false;
                }
            }
        }

    }
}