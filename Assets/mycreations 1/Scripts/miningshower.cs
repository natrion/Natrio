using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miningshower : MonoBehaviour
{
    [SerializeField] private NetworkComunicator NetworkComunicator;
    public GameObject chunkparent;
    private GameObject selectedthing;
    public GameObject copyobjectsfolder;
    Animator m_Animator;

    private GameObject select_zone_selected;
    public GameObject player;
    private Transform textHealthcopy;
    private MeshRenderer rend;

    private AudioSource[] Audio;
    private AudioSource Punch;
    private AudioSource PunchToNothing;
    void Start()
    {
        Audio = transform.parent.GetComponents<AudioSource>();
        Punch = Audio[0];
        PunchToNothing = Audio[2];
    }
    //private float distance;
    //private Plane plane;
    void Update()
    {
        if(PauseScript.paused==true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            PunchToNothing.Play();
        }

        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        
        if (hit.collider != null)
        {
            if(selectedthing)
            {
                if(selectedthing != hit.collider.gameObject)
                {
                    Destroy(select_zone_selected);
                    rend.enabled = false;
                }
            }

           
            if (hit.collider.gameObject.tag == "Untagged" | hit.collider.gameObject.name == "MainCamera" | hit.collider.gameObject.name == "Axe" | hit.collider.gameObject.name == "wheelbarrow")
            {
                if (rend & select_zone_selected)
                {
                    if (selectedthing != hit.collider.gameObject)
                    {
                        Destroy(select_zone_selected);
                        rend.enabled = false;
                    }                    
                }
            }        
            else if(hit.collider.transform.childCount > 0)
            {         
                if(hit.collider.transform.GetChild(0).GetComponent<MeshRenderer>())

                if (player.transform.position.y - hit.collider.transform.position.y < 1f & player.transform.position.y - hit.collider.transform.position.y > -1f & player.transform.position.x - hit.collider.transform.position.x > -1f & player.transform.position.x - transform.position.x < 1f)
                {
                    textHealthcopy = hit.collider.transform.GetChild(0);
                    m_Animator = hit.collider.gameObject.GetComponent<Animator>();
                    rend = textHealthcopy.GetComponent<MeshRenderer>();

                    selectedthing = hit.collider.gameObject;
                    //doingrealmining
                    if (Input.GetMouseButtonDown(0))
                    {
                        Punch.Play();
                        string tag = hit.collider.gameObject.tag;
                        string tofind = tag + "_full";
                        Transform found = copyobjectsfolder.transform.Find(tofind);
                        GameObject copyObject = Instantiate(found.gameObject);
                        copyObject.tag = "Untagged";
                        copyObject.transform.position = hit.collider.transform.position;
                        copyObject.transform.parent = chunkparent.transform;
                        Destroy(hit.collider.gameObject);
                        if (hit.collider.gameObject.GetComponent<dataHolder>() != null)
                        {
                            dataHolder DataHolder = copyObject.AddComponent(typeof(dataHolder)) as dataHolder;
                            DataHolder.dataIdentificator = hit.collider.gameObject.GetComponent<dataHolder>().dataIdentificator;
                            NetworkComunicator.AllObjects[hit.collider.gameObject.GetComponent<dataHolder>().dataIdentificator] = copyObject;
                        }
                       

                        return;
                        
                    }
                    //selectshow
                    int number_of_player_childs = hit.collider.transform.childCount;

                    if (number_of_player_childs == 1)
                    {
                            string tofind = hit.collider.gameObject.tag + "_full";
                              
                        select_zone_selected = Instantiate(copyobjectsfolder.transform.Find(tofind).GetComponent<mining>().select_zone);
           
                        select_zone_selected.transform.position = hit.collider.transform.position;
                        select_zone_selected.transform.parent = hit.collider.transform;

                        rend.enabled = true;
                        
                    }
                }
                //selescthide
                if (player.transform.position.y - hit.collider.transform.position.y > 1f || player.transform.position.y - hit.collider.transform.position.y < -1f || player.transform.position.x - hit.collider.transform.position.x < -1f || player.transform.position.x - hit.collider.transform.position.x > 1f)
                {
                    if (rend & select_zone_selected)
                    {
                        Destroy(select_zone_selected);
                        rend.enabled = false;
                    }
                }
            }
        }else 
        {
            if (rend & select_zone_selected)
            {
                Destroy(select_zone_selected );
                rend.enabled = false;
            }            
        }
    }
}
