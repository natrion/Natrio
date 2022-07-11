using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mining : MonoBehaviour
{
    Animator m_Animator;
    public float health ;
    private float healthpercent;
    public GameObject item;
    public GameObject folderitems;
    private int damage=1;
    public GameObject select_zone;
    private GameObject select_zone_selected;
    public GameObject player;
    private Transform textHealthcopy;
    private MeshRenderer rend;
    public int how_many_items;
    public GameObject item2;
    public int how_many_items2;
    private float healthstart ;
    public GameObject item3;
    public int how_many_items3;

    void Start()
    {
        
        textHealthcopy = transform.GetChild(0);
        rend = textHealthcopy.GetComponent<MeshRenderer>();
        textHealthcopy.GetComponent<TextMesh>().text = health.ToString();
        rend.enabled = false;

        m_Animator = gameObject.GetComponent<Animator>();
        healthstart = health;
    }

    void OnMouseEnter()
    {
        if (player.transform.position.y - transform.position.y < 1f & player.transform.position.y - transform.position.y > -1f & player.transform.position.x - transform.position.x > -1f & player.transform.position.x - transform.position.x < 1f)
        {
            select_zone_selected = Instantiate(select_zone);
            select_zone_selected.transform.position = transform.position;
            select_zone_selected.transform.parent = transform;

            rend.enabled = true;
        }
    }

    void OnMouseOver()
    {
        
        if (player.transform.position.y - transform.position.y < 1f & player.transform.position.y - transform.position.y > -1f & player.transform.position.x - transform.position.x > -1f & player.transform.position.x - transform.position.x < 1f)
        {
            int number_of_player_childs = transform.childCount;
            if (number_of_player_childs ==1)
            {
                select_zone_selected = Instantiate(select_zone);
                select_zone_selected.transform.position = transform.position;
                select_zone_selected.transform.parent = transform;

                rend.enabled = true;    
            }
        }

        if (player.transform.position.y - transform.position.y > 1f || player.transform.position.y - transform.position.y < -1f || player.transform.position.x - transform.position.x < -1f || player.transform.position.x - transform.position.x > 1f)
        {
            Destroy(select_zone_selected);

            rend.enabled = false;
        }

        if (Input.GetMouseButtonDown(0) &  player.transform.position.y - transform.position.y < 1f & player.transform.position.y - transform.position.y > -1f & player.transform.position.x - transform.position.x > -1f & player.transform.position.x - transform.position.x < 1f)
        {
            damage = FindObjectOfType<holding>().what_damage; 
            health -= damage;
            healthpercent = ((health * 5) / healthstart);
            m_Animator.SetFloat("destroyd", 5 - healthpercent);
            if (textHealthcopy)
            {
                textHealthcopy.GetComponent<TextMesh>().text = health.ToString();
            }
            
            
            /////////////////////////////////////////////////////////////////////item
            if (health < 1 & item)
            {
                for (int i = 0; i < how_many_items; i++)
                { 
                    GameObject duplicated = Instantiate(item);
                    duplicated.transform.position = new Vector3(gameObject.transform.position.x + Random.Range(-0.5f, 0.5f), gameObject.transform.position.y + Random.Range(-0.5f, 0.5f), 0);
                    duplicated.transform.eulerAngles = Vector3.forward * Random.Range(0.01f, 360f);
                    duplicated.transform.parent = folderitems.transform;
                }              
                Destroy(gameObject);
            }
            /////////////////////////////////////////////////////////////////////item2
            if (health < 1 & item2)
            {
                for (int i = 0; i < how_many_items2; i++)
                {
                    GameObject duplicated = Instantiate(item2);
                    duplicated.transform.position = new Vector3(gameObject.transform.position.x + Random.Range(-0.5f, 0.5f), gameObject.transform.position.y + Random.Range(-0.5f, 0.5f), 0);
                    duplicated.transform.eulerAngles = Vector3.forward * Random.Range(0.01f, 360f);
                    duplicated.transform.parent = folderitems.transform;
                }
                Destroy(gameObject);
            }
            ////////////////////////////////////////////////////////////////////////////item3
            if (health < 1 & item3)
            {
                for (int i = 0; i < how_many_items3; i++)
                {
                    GameObject duplicated = Instantiate(item3);
                    duplicated.transform.position = new Vector3(gameObject.transform.position.x + Random.Range(-0.5f, 0.5f), gameObject.transform.position.y + Random.Range(-0.5f, 0.5f), 0);
                    duplicated.transform.eulerAngles = Vector3.forward * Random.Range(0.01f, 360f);
                    duplicated.transform.parent = folderitems.transform;
                }
                Destroy(gameObject);
            }
        }
    }
    
    void OnMouseExit()
    {
        Destroy(select_zone_selected);
        rend.enabled = false;
    }
}
