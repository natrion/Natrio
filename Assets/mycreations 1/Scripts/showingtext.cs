
using UnityEngine;
using UnityEngine.UI;

public class showingtext : MonoBehaviour
{
    public Text coinstext;
    public void showtext(int whatshow)
    {

        string whatshowstring = whatshow.ToString();
        coinstext.text = whatshowstring +"c" ;
        
    }
}
