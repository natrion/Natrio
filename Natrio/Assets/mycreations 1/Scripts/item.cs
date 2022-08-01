using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class item : MonoBehaviour
{
    public bool cutingpisible;
    Animator m_Animator;
    public Transform player;
    public int how_much_cost = 1;
    public float StartRotation;
    public float HoldingDistanceFromPlayer;
    public float PickUpRadius;
    public int damage;
    public bool side;
    public bool isItForTransportingItems;
    public int howMuchItemsTransporting;
    public bool xRotation;
    public bool yRotation;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();   
    }
    void Update()
    {
        if(side == true )
        {
            if(player == transform.parent)
            {
                if (Input.GetAxisRaw("Horizontal") == 1 | Input.GetAxisRaw("Horizontal") == -1)
                {
                    m_Animator.SetBool("side", true);
                }
                else if (Input.GetAxisRaw("Vertical") == 1 | Input.GetAxisRaw("Vertical") == -1)
                {
                    m_Animator.SetBool("side", false);
                }
            }
        }
    }
    void OnMouseEnter()
    {
        if (player.transform.position.y - transform.position.y < PickUpRadius & player.transform.position.y - transform.position.y > PickUpRadius*-1 & player.transform.position.x - transform.position.x > PickUpRadius * -1 & player.transform.position.x - transform.position.x < PickUpRadius)
        {
            m_Animator.SetBool("isselect", true);
            FindObjectOfType<holding>().isMouseOver = true;
        }         
    }

    void OnMouseExit()
    {
        m_Animator.SetBool("isselect", false);
        FindObjectOfType<holding>().isMouseOver = false;
    }

    void OnMouseOver()
    {
        int number_of_player_childs = player.childCount;

        if (number_of_player_childs == 0)
        {
            FindObjectOfType<holding>().itemholding(HoldingDistanceFromPlayer, StartRotation, transform, how_much_cost, cutingpisible, gameObject, damage, PickUpRadius, xRotation,yRotation);
        }
        else if(player.GetChild(0).GetComponent<item>().isItForTransportingItems)
        {
            if (player.GetChild(0).GetComponent<item>().isItForTransportingItems == true & player.GetChild(0).GetComponent<item>().howMuchItemsTransporting > player.GetChild(0).childCount & Input.GetMouseButtonDown(1) &  isItForTransportingItems == false)
            {
                transform.parent = player.GetChild(0);
                transform.position = player.position;
                transform.position += player.up * HoldingDistanceFromPlayer*2;
                transform.eulerAngles = player.eulerAngles - new Vector3(0, 0, StartRotation);
            }
            
        }
        return;
    
    }
}