using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miningshower : MonoBehaviour
{
    public GameObject copyobjectsfolder;
    Animator m_Animator;
    public GameObject select_zone;
    private GameObject select_zone_selected;
    public GameObject player;
    private Transform textHealthcopy;
    private MeshRenderer rend;
    private bool selectbefore = false;
    //private float distance;
    //private Plane plane;
    void Update()
    {
        //void OnMouseStart



        //void OnMouseOver()


        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Untagged" | hit.collider.gameObject.name == "MainCamera")
            {
                if (rend & rend)
                {
                    Destroy(select_zone_selected);
                    rend.enabled = false;
                }
            }
            else
            { 

                textHealthcopy = hit.collider.transform.GetChild(0);
                m_Animator = hit.collider.gameObject.GetComponent<Animator>();
                rend = textHealthcopy.GetComponent<MeshRenderer>();

                
                if (player.transform.position.y - hit.collider.transform.position.y < 1f & player.transform.position.y - hit.collider.transform.position.y > -1f & player.transform.position.x - hit.collider.transform.position.x > -1f & player.transform.position.x - transform.position.x < 1f)
                {
                    //doingrealmining
                    if(Input.GetMouseButtonDown(0))
                    {
                        string tag = hit.collider.gameObject.tag;
                        string tofind = tag + "_full";
                        Transform found = copyobjectsfolder.transform.Find(tofind);
                        GameObject copyObject = Instantiate(found.gameObject);
                        copyObject.tag = "Untagged";
                        copyObject.transform.position = hit.collider.transform.position;
                        Destroy(hit.collider.gameObject);
                        selectbefore = false;
                        return;
                        
                    }
                    //selectshow
                    int number_of_player_childs = hit.collider.transform.childCount;

                    if (number_of_player_childs == 1)
                    {
                        select_zone_selected = Instantiate(select_zone);
                        select_zone_selected.transform.position = hit.collider.transform.position;
                        select_zone_selected.transform.parent = hit.collider.transform;

                        rend.enabled = true;
                        selectbefore = true;
                    }
                }
                //selescthide
                if (player.transform.position.y - transform.position.y > 1f || player.transform.position.y - transform.position.y < -1f || player.transform.position.x - transform.position.x < -1f || player.transform.position.x - transform.position.x > 1f)
                {
                    Destroy(select_zone_selected);

                    rend.enabled = false;
                }
            }
        }else 
        {
            if (rend & rend)
            {
                Destroy(select_zone_selected );
                rend.enabled = false;
            }

            
        }
    }
}
