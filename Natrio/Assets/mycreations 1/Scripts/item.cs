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
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();   
    }

    void OnMouseEnter()
    {
        if (player.transform.position.y - transform.position.y < PickUpRadius & player.transform.position.y - transform.position.y > PickUpRadius*-1 & player.transform.position.x - transform.position.x > PickUpRadius * -1 & player.transform.position.x - transform.position.x < PickUpRadius)
        {
            m_Animator.SetBool("isselect", true);
        }         
    }

    void OnMouseExit()
    {
        m_Animator.SetBool("isselect", false);
    }

    void OnMouseOver()
    {
        int number_of_player_childs = player.childCount;

        if (number_of_player_childs == 0)
        {
            FindObjectOfType<holding>().itemholding(HoldingDistanceFromPlayer, StartRotation, transform, how_much_cost, cutingpisible, gameObject, damage, PickUpRadius);
        }
        return;
    
    }
}
