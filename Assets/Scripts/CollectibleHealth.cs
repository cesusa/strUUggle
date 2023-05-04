using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectibleHealth : MonoBehaviour
{
    public HealthBar healthbar;

    private void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
