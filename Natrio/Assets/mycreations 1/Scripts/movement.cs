using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Animator animator;
    public float movespeed = 2f;
    public Rigidbody2D rb;
    Vector2 movement2;
    public Transform playertransform;
    private bool isdawn = true;
    public GameObject player;
    const string LAYER_NAME1 = "player";
    const string LAYER_NAME2 = "playerbehinditem";

    public void changespeed(float setspeed)
    {
        movespeed = setspeed;
    }

    IEnumerator clickanimation()
    {
         if (movement2.x == 0 & movement2.y == 0)
         {
            int number_of_player_childs = transform.childCount;
            if (number_of_player_childs == 0)
            {
                animator.SetBool("hit", true);
                yield return new WaitForSeconds(1);

                animator.SetBool("hit", false);
            }
         }
        
    }

    void Update()
    {
        
        movement2.x = Input.GetAxisRaw("Horizontal");
        movement2.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {          
            StartCoroutine(clickanimation());
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        SpriteRenderer mySpriteRenderer = player.GetComponent<SpriteRenderer>();
        rb.MovePosition(rb.position + movement2 * movespeed * Time.fixedDeltaTime);
        

          //-x...........................................................................
        if (movement2.x == -1)
        {
            animator.SetInteger("front1back2side3", 3);
            animator.SetBool("move", true);
            transform.eulerAngles = Vector3.forward * 90;
            playertransform.localScale = new Vector3(-1, 1, 0);
            if (isdawn == false)
            {
                transform.position += playertransform.up * 0.15f;
                isdawn = true;
            }
            mySpriteRenderer.sortingLayerName = LAYER_NAME1;
        }
        //x...........................................................................
        else if (movement2.x == 1)
        {
            animator.SetInteger("front1back2side3", 3);
            animator.SetBool("move", true);
            transform.eulerAngles = Vector3.forward * 270;
            playertransform.localScale = new Vector3(1, 1, 0);
            if (isdawn == false)
            {
                transform.position += playertransform.up * 0.15f;
                isdawn = true;
            }
            mySpriteRenderer.sortingLayerName = LAYER_NAME1;
        }
        //-y...........................................................................
        else if (movement2.y == -1)
        {
            animator.SetInteger("front1back2side3", 1);
            animator.SetBool("move", true);
            transform.eulerAngles = Vector3.forward * 180;
            int number_of_player_childs = transform.childCount;
            if (number_of_player_childs == 1)
            {
                mySpriteRenderer.sortingLayerName = LAYER_NAME2;
            }
            if (isdawn == false)
            {
                transform.position += playertransform.up * 0.15f;
                isdawn = true;
            }
        }
        //y...........................................................................
        else if (movement2.y == 1)
        {
            animator.SetInteger("front1back2side3", 2);
            animator.SetBool("move", true);
            transform.eulerAngles = Vector3.forward * 0;
            
            mySpriteRenderer.sortingLayerName = LAYER_NAME1;

            if (isdawn == true )
            { transform.position -= playertransform.up * 0.15f;
                isdawn = false;
            }
            
        }
        else if (movement2.x == 0 & movement2.y == 0)
        {
            animator.SetBool("move", false);
        }

    }   

}
