using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class item : MonoBehaviour
{

    public Transform itemtest;
    public sellthings SellScript;
    private GameObject folderforsale;

    public GameObject itemfolder;

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
    private Rigidbody2D rb;
    private BuildMode BuildModeScript;
    private Vector3 moveTo;
    private int EnterBoxColider;
    private bool FunctionRunning = false;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<BuildMode>() != null)
        {
            BuildModeScript = collider.GetComponent<BuildMode>();

            if (BuildModeScript.isConveyorBelt == true & transform.parent != player & BuildModeScript.isInbildingMode == false & BuildModeScript.isTurnConveyorBelt == false)
            {
                if (rb.mass > 2 | rb.mass == 2)
                {
                    moveTo = collider.transform.up * -BuildModeScript.ConveyorSpeed / rb.mass;
                } else
                {
                    moveTo = collider.transform.up * -BuildModeScript.ConveyorSpeed / 2;
                }
                rb.MovePosition(rb.position + new Vector2(moveTo.x, moveTo.y));
            }
            else if (BuildModeScript.itemSorter == true & BuildModeScript.isInbildingMode == false & transform.parent != player & FunctionRunning == false)
            {
                float pointerrotation = collider.transform.GetChild(0).eulerAngles.z;
                if(pointerrotation == 0 ) { EnterBoxColider = 2; }
                if (pointerrotation == 180) { EnterBoxColider = 0; }
                if (pointerrotation == 270 ) { EnterBoxColider = 1; }
                if (pointerrotation == 90 ) { EnterBoxColider = 3; }


                BoxCollider2D[] coliders = collider.GetComponents<BoxCollider2D>();
                BoxCollider2D EnterItemFilterCollider = coliders[EnterBoxColider];

                if(EnterItemFilterCollider == collider)
                {
                    FunctionRunning = true;
                    Transform pointer= collider.transform.GetChild(0);

                    if (collider.transform.GetChild(1).GetComponent<TextMesh>().text == "Not Sorting")
                    {
                        float RandomNumber = Random.Range(0f, 4f);

                        if (RandomNumber < 1)
                        {
                            transform.position = collider.transform.position - pointer.up * 0.4f;
                        }
                        if (RandomNumber > 1 & RandomNumber < 2)
                        {
                            transform.position = collider.transform.position + pointer.right * 0.4f;
                        }
                        if (RandomNumber > 3)
                        {
                            transform.position = collider.transform.position - pointer.right * 0.4f;
                        }
                    }
                    else
                    {
                        if ("Sorting " + gameObject.tag == collider.transform.GetChild(1).GetComponent<TextMesh>().text)
                        {
                            transform.position = transform.position = collider.transform.position - pointer.up * 0.4f;
                        }
                        else
                        {
                            float RandomNumber = Random.Range(0f, 2f);

                            if (RandomNumber < 1)
                            {
                                transform.position = collider.transform.position + pointer.right * 0.4f;
                            }
                            if (RandomNumber > 1)
                            {
                                transform.position = collider.transform.position - pointer.right * 0.4f;
                            }
                        }
                    }
                    FunctionRunning = false;
                }
            }
            else if (BuildModeScript.isItemConvertor == true & BuildModeScript.isInbildingMode == false & transform.parent != player & FunctionRunning == false)
            {
                FunctionRunning = true;
                for (int i = 0; i < BuildModeScript.ConvertingItemTag.Length; i++)
                {
                    if (gameObject.CompareTag(BuildModeScript.ConvertingItemTag[i]))
                    {
                        StartCoroutine(itemConvertor(BuildModeScript , i));
                    }
                }                               
            }
        }
        else if (collider.transform.parent.GetComponent<BuildMode>() != null)
        {
            BuildModeScript = collider.transform.parent.GetComponent<BuildMode>();
            if (BuildModeScript.isConveyorBelt == true & transform.parent != player & BuildModeScript.isInbildingMode == false)
            {
                Vector3 moveTo = collider.transform.up * -BuildModeScript.ConveyorSpeed / rb.mass;
                rb.MovePosition(rb.position + new Vector2(moveTo.x, moveTo.y));
            }        
        }
    }
    IEnumerator itemConvertor(BuildMode curentBuildModeScript , int receptNumber)
    {        
        yield return new WaitForSeconds(curentBuildModeScript.TimeToConvert[receptNumber]);

        float xdistance = transform.position.x - curentBuildModeScript.transform.position.x;
        float ydistance = transform.position.y - curentBuildModeScript.transform.position.y;

        if (xdistance<1 & xdistance > -1    &   ydistance < 1 & ydistance > -1)
        {
            Transform iemToCopy = itemtest.transform.GetChild(curentBuildModeScript.CreatingFromConvertingItemNumber[receptNumber]);
            GameObject CopyCreatingFromConvertingItem = Instantiate(iemToCopy.gameObject);
            CopyCreatingFromConvertingItem.transform.parent = itemfolder.transform;
            CopyCreatingFromConvertingItem.transform.position = curentBuildModeScript.transform.position - curentBuildModeScript.transform.up * 0.4f;

            FunctionRunning = false;

            Destroy(gameObject);
        }
        else { FunctionRunning = false; }        
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

        if(transform.parent != player & transform.parent.GetComponent< item>() == null)
        {
            if(transform.parent != itemtest)

            if (transform.position.y < 1.4f & transform.position.y > 0.4f & transform.position.x > -1.4f & transform.position.x < -0.4f)
            {
                if(how_much_cost !=0 & SellScript.foldersell != null)
                {
                    transform.parent = SellScript.foldersell.transform;
                    SellScript.addcoinspotencial();
                } 
            }
            else if (transform.parent != itemfolder)
            {
                transform.parent = itemfolder.transform;
                SellScript.addcoinspotencial();
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

        if (PauseScript.paused == true)
        {
            return;
        }
        int number_of_player_childs = player.childCount;

        if (number_of_player_childs == 0)
        {
            if(Input.GetMouseButtonDown(1))
            { FindObjectOfType<holding>().itemholding(HoldingDistanceFromPlayer, StartRotation, transform, how_much_cost, cutingpisible, gameObject, damage, PickUpRadius, xRotation, yRotation); }
            
        }
        else if(player.GetChild(0).GetComponent<item>().isItForTransportingItems)
        {
            if(Input.GetMouseButtonDown(1) | Input.GetMouseButtonDown(2))
            {
                if (player.GetChild(0).GetComponent<item>().isItForTransportingItems == true & player.GetChild(0).GetComponent<item>().howMuchItemsTransporting > player.GetChild(0).childCount & isItForTransportingItems == false)
                {
                    if (transform.GetComponent<Rigidbody2D>() != null)
                    {
                        transform.GetComponent<Rigidbody2D>().simulated = false;
                    }

                    transform.parent = player.GetChild(0);
                    transform.position = player.position;
                    transform.position += player.up * HoldingDistanceFromPlayer * 2;
                    transform.eulerAngles = player.eulerAngles - new Vector3(0, 0, StartRotation);
                }
            }                       
        }
        return;    
    }
}