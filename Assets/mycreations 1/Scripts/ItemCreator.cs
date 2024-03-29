using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
    [SerializeField] private NetworkComunicator NetworkComunicator;
    private GameObject copy;

    private AudioSource[] Audio;
    private AudioSource isrunning;
    private AudioSource CutATree;
    private AudioSource flickbuton;

    private float Xdistance;
    private float Ydistance;
    private float Distance;

    public Transform player;


    public static bool BuildModeIsRunning = false;    
    public Transform itemFolder;
    public GameObject ObjectToCreate;
    public GameObject ObjectToCreate2;
    public float time;
    public float distance;

    private bool OnOre = false;
    public bool isMiner;
    public string[] ProduseOnThisTag;
    public GameObject[] ProduseThis;
    private int onWhatOre = -1;

    private Animator m_Animator;
    private Vector3 PositionOnStrat;
    private float x;
    private float y;
    public bool On = false;
    private SpriteRenderer PointerRend;
    private bool functionRunning =false;

    void Start()
    {
        PointerRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Audio = itemFolder.GetComponents<AudioSource>();
        isrunning = Audio[2];
        CutATree = Audio[3];
        flickbuton = Audio[4];

        BuildModeIsRunning = false;
        
        m_Animator = gameObject.GetComponent<Animator>();

        

        if (On ==true)
        {
            isrunning.Play();
            m_Animator.SetBool("On", true);

            if (functionRunning == false)
            {
                StartCoroutine(CreteItems());
            }
        }

        StartCoroutine(IsOnDeposit());
    }
    IEnumerator IsOnDeposit()
    {
        Vector3 wasPos = transform.position;
        transform.position += new Vector3(1, 0, 0);

        yield return new WaitForSeconds(0.0001f);

        RaycastHit2D hit = Physics2D.Raycast(wasPos, Vector2.zero);

        if (isMiner == true & hit.collider != null)
        {
            onWhatOre = -1;
            OnOre = false;
            for (int i = 0; i<ProduseOnThisTag.Length; i++)
            {
                if (hit.collider.gameObject.CompareTag(ProduseOnThisTag[i]))
                {
                    OnOre = true;
                    onWhatOre = i;
                }
            }
        }
        
        transform.position -= new Vector3(1, 0, 0);
    }

    void Update()
    {
        if(On == true)
        {
            if (!isrunning.isPlaying)
            {
                isrunning.Play();
            }
            Xdistance = transform.position.x - player.position.x;
            if (Xdistance < 0f) { Xdistance = Xdistance * -1; }

            Ydistance = transform.position.y - player.position.y;
            if (Ydistance < 0f) { Ydistance = Ydistance * -1; }

            Distance = Xdistance + Ydistance;

            if (Distance < 2)
            {
                isrunning.volume = ((2 - Distance) / 2) / 4;
            }
        }
    }
    void OnMouseExit()
    {
        PointerRend.enabled = false;
    }

    void OnMouseOver()
    {
        PointerRend.enabled = true;

        if (Input.GetMouseButtonDown(1) & BuildModeIsRunning ==false )
        {
            if(On == false )
            {

                isrunning.Play();
                flickbuton.Play();

                On = true;
                m_Animator.SetBool("On", true);

                if (functionRunning ==false)
                {
                    StartCoroutine(CreteItems());
                }
            }
            else
            {
                isrunning.Stop();
                flickbuton.Play();
                On = false;
                m_Animator.SetBool("On", false);
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (BuildModeIsRunning == false)
            {
                transform.GetComponent<BoxCollider2D>().isTrigger = true;
                On = false;
                m_Animator.SetBool("On", false);
                m_Animator.SetBool("Select", true);
                BuildModeIsRunning = true;
                StartCoroutine(BuildModeItemCreate());
            }        
        }
    }
    IEnumerator BuildModeItemCreate()
    {
        NetworkComunicator.SentInformation(false, false, true, 0, transform);
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
                if (hitXy.collider.isTrigger ==false)
                {
                    if (hitXy.collider.transform.position != transform.position) { m_Animator.SetBool("SelectRed", true); }
                }                    
            }
            else { m_Animator.SetBool("SelectRed", false); }

            transform.position = new Vector3(x, y, 0);
            
            yield return new WaitForSeconds(0.0001f);

            if(Input.GetKeyDown(KeyCode.R))
            {
                transform.GetChild(0).eulerAngles -= Vector3.forward * 90; ;
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + 1,0);

        yield return new WaitForSeconds(0.0001f);

        Vector2 BuildModeAutput = new Vector2(x, y);
        RaycastHit2D hit = Physics2D.Raycast(BuildModeAutput, Vector2.zero);

        if (hit.collider != null)
        {
            if(isMiner == true)
            {
                onWhatOre = -1;
                OnOre = false;
                for (int i = 0; i < ProduseOnThisTag.Length; i++)
                {
                    if(hit.collider.gameObject.CompareTag(ProduseOnThisTag[i]))
                    {
                        OnOre = true;
                        onWhatOre = i;
                    }
                }           
            }

            if (OnOre == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
            }
            else
            {
                transform.position = PositionOnStrat;
                
            }

        }
        else
        { 
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
            onWhatOre = -1;
        }

        BuildModeIsRunning = false;
        transform.GetComponent<BoxCollider2D>().isTrigger = false;
        m_Animator.SetBool("SelectRed", false);
        m_Animator.SetBool("Select", false);
        NetworkComunicator.SentInformation(false, true, false, 0, transform);
    }
    IEnumerator CreteItems()
    {
        
        while (On == true)
        {
            functionRunning = true;

            m_Animator.SetFloat("PercentToFinish",51 );

            yield return new WaitForSeconds(time / 2);
            if (On == true) 
            {
                if (isMiner == true)
                {
                    if (onWhatOre != -1)
                    {
                        copy = Instantiate(ProduseThis[onWhatOre]);
                    }
                    else { copy = Instantiate(ObjectToCreate);  }
                }
                else { copy = Instantiate(ObjectToCreate); }

                
                copy.transform.parent = itemFolder;
                copy.transform.position = transform.position ;
                copy.transform.position += transform.GetChild(0).up* -distance;

                if(ObjectToCreate2 !=null)
                {
                    GameObject copy2 = Instantiate(ObjectToCreate2);
                    copy2.transform.parent = itemFolder;
                    copy2.transform.position = transform.position;
                    copy2.transform.position += transform.GetChild(0).up * -distance;
                }

                Xdistance = transform.position.x - player.position.x;
                if (Xdistance < 0f) { Xdistance = Xdistance * -1; }

                Ydistance = transform.position.y - player.position.y;
                if (Ydistance < 0f) { Ydistance = Ydistance * -1; }

                Distance = Xdistance + Ydistance;

                if (Distance < 4)
                {
                    CutATree.volume = ((4 - Distance) / 4) / 2;
                    CutATree.Play();
                }
            }

            m_Animator.SetFloat("PercentToFinish", 0);m_Animator.SetFloat("PercentToFinish", 0);

            yield return new WaitForSeconds(time / 2);
        }
        functionRunning = false;
    }
}

