using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleController : MonoBehaviour
{


    public static int coinCount;
    public GameObject coinCountDisplay;
    
    
    

    // Update is called once per frame
    void Update()
    {
        coinCountDisplay.GetComponent<TextMeshProUGUI>().text = "" + coinCount;
       
    }
}
