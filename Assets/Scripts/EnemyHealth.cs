using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int FullHealth;
    //int currentHelath;
     
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FullHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void TakeDamage(int damage)
    {
        FullHealth -= damage;
        Debug.Log(FullHealth);
        /*if( currentHelath <=0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            Debug.Log("oldu");
            
        }*/
    }
}
