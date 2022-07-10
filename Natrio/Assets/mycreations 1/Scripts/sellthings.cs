using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sellthings : MonoBehaviour
{
    public Animator m_Animator;
    public int how_many_coins = 0;
    private int how_much_coins_on_platform = 0;
    private GameObject foldersell;
    public TextMesh on_platform_text;
    private int coinsfromloop;
    private int price_ofchild;
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
            
            Transform child= foldersell.transform.GetChild(i);

            if(child.GetComponent<item>())
            {  price_ofchild = child.GetComponent<item>().how_much_cost; }

            if (child.GetComponent<axecut>())
            {  price_ofchild = child.GetComponent<axecut>().how_much_cost; }

            coinsfromloop += price_ofchild;
            
        }
        how_much_coins_on_platform = coinsfromloop;
        string whatshowstring = how_much_coins_on_platform.ToString();
        on_platform_text.text = whatshowstring;
        coinsfromloop = 0;
    }
    void OnMouseOver()
    {
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
    }
}
