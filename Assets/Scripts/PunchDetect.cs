using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PunchDetect : MonoBehaviour
{
    bool isPunching;
    bool PunchAnim;
    




    // Start is called before the first frame update
    void Start()
    {
        
        PunchAnim = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            PunchAnim = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy"  && PunchAnim == true )
        {
            UnityEngine.Debug.Log("Vurdu");
            other.GetComponent<EnemyHealth>().TakeDamage(5);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        PunchAnim = false;
    }
    private void OnTriggerExit(Collider other)
    {
        PunchAnim = false;
    }


}
