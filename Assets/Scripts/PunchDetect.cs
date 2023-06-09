using UnityEngine;

public class PunchDetect : MonoBehaviour
{
    bool isPunching;
    bool PunchAnim;
    
    void Start()
    {
        PunchAnim = false;
    }

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
