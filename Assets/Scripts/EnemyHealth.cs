using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int FullHealth;
    int currentHelath;
     
    
    // Start is called before the first frame update
    void Start()
    {
        currentHelath = FullHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        currentHelath -= damage;
        Debug.Log(currentHelath);
        if( currentHelath <=0)
        {
            Destroy(gameObject);
            
        }
    }
}
