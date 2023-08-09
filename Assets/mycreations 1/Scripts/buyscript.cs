using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyscript : MonoBehaviour
{
    [SerializeField] private NetworkComunicator networkComunicator;
    public AudioSource Audio;
    public Animator m_Animator;
    private GameObject foldersell;
    private int money;
    public int cost;
    public GameObject What_buy;
    public GameObject folderitems;

    IEnumerator pressed()
    {

        m_Animator.SetBool("presd", true);
        yield return new WaitForSeconds(1);

        m_Animator.SetBool("presd", false);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (PauseScript.paused == true)
            {
                return;
            }

            StartCoroutine(pressed());
            print("pressed");
            money = FindObjectOfType<sellthings>().how_many_coins;
            if (money>cost-1)
            {
                Audio.Play();
                FindObjectOfType<sellthings>().addcoins(cost * -1);
                GameObject boughtobject = Instantiate(What_buy);
                boughtobject.transform.parent = folderitems.transform;
                boughtobject.transform.position = new Vector3(1, 2, 0);
                networkComunicator.SentInformation(false, true, false,0, boughtobject.transform);
            }
        }
    }

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }
}
