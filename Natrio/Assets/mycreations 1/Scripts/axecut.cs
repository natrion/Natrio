using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axecut : MonoBehaviour
{
    public bool cutingpisible;
    Animator m_Animator;
    public Transform player;
    public int how_much_cost = 2;

    
    void Start()
    { m_Animator = gameObject.GetComponent<Animator>(); }
    
    void OnMouseEnter()
    {
        if (player.transform.position.y - transform.position.y < 0.5f & player.transform.position.y - transform.position.y > -0.5f & player.transform.position.x - transform.position.x > -0.5f & player.transform.position.x - transform.position.x < 0.5f)
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
            FindObjectOfType<holding>().itemholding(0.1f, 180f, transform, how_much_cost, cutingpisible, gameObject,2,0.5f);
        }
        return;
    }
}
