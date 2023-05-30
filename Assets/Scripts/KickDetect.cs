using UnityEngine;

public class KickDetect : MonoBehaviour
{
    bool KickAnim;
    
    void Start()
    {
        KickAnim = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            KickAnim = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy"  && KickAnim == true )
        {
            Debug.Log("Kick!");
            other.GetComponent<EnemyHealth>().TakeDamage(5);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        KickAnim = false;
    }
    
    private void OnTriggerExit(Collider other)
    {
        KickAnim = false;
    }


}