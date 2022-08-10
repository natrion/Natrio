using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sellthings : MonoBehaviour
{
    public Animator m_Animator;
    public int how_many_coins = 0;
    private int how_much_coins_on_platform = 0;
    public GameObject foldersell;
    public TextMesh on_platform_text;
    private int coinsfromloop;
    private int price_ofchild;
    public Transform FolderForItems;
    private int price_ofchildTransportItem;
    private Transform child;
    IEnumerator pressed()
    {
        m_Animator.SetBool("presd", true);
        yield return new WaitForSeconds(1);

        m_Animator.SetBool("presd", false);
    }
    public void addcoins(int howmuchadd)
    {
        how_many_coins += howmuchadd;
        FindObjectOfType<showingtext>().showtext(how_many_coins);
        print(how_many_coins);
    }
    public void addcoinspotencial ()
    {
        int number_of_player_childs = foldersell.transform.childCount;
        for (int i =0;  i < number_of_player_childs; i++)
        {
            
            child= foldersell.transform.GetChild(i);
            price_ofchild = child.GetComponent<item>().how_much_cost; 

            if(price_ofchild == 0)
            { child.parent = FolderForItems; }

            if (child.GetComponent<item>().isItForTransportingItems == true)
            {
                int number_transport_item_childs = child.transform.childCount;

                for (int o = 0; o < number_transport_item_childs; o++)
                {
                    Transform childTransportItem = child.transform.GetChild(0);
                    price_ofchildTransportItem = childTransportItem.GetComponent<item>().how_much_cost;

                    if (price_ofchildTransportItem == 0)
                    { childTransportItem.parent = FolderForItems; }

                    price_ofchild += price_ofchildTransportItem;
                    childTransportItem.parent = foldersell.transform;
                    if (childTransportItem.GetComponent<Rigidbody2D>() != null)
                    {
                        childTransportItem.GetComponent<Rigidbody2D>().simulated = true;
                    }

                }
            }
            coinsfromloop += price_ofchild;
        }
        how_much_coins_on_platform = coinsfromloop;
        string whatshowstring = how_much_coins_on_platform.ToString();
        on_platform_text.text = whatshowstring;
        coinsfromloop = 0;
    }
    void OnMouseOver()
    {
        if (PauseScript.paused == true)
        {
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(pressed());

            Destroy(foldersell);
            foldersell = new GameObject("thingsforsell");
            foldersell.transform.parent = gameObject.transform.parent;            
            
            how_many_coins += how_much_coins_on_platform;
                        
            FindObjectOfType<showingtext>().showtext(how_many_coins);

            addcoinspotencial();
        }
    }
    void Start()
    {
        foldersell = new GameObject("thingsforsell");
        foldersell.transform.parent = gameObject.transform.parent;
        FindObjectOfType<showingtext>().showtext(how_many_coins);
    }
}
