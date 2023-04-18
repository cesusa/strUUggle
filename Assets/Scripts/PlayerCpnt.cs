using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCpnt : MonoBehaviour
{

    public bool isAttacking;
    private Animator animator;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isAttacking)
            {
                
                animator.SetTrigger("Punch1");
                
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (!isAttacking)
            {
                
                animator.SetTrigger("Kick");
                
            }
        }
    }
    public void StartAttacking()
    {
        isAttacking = true;
    }
    public void FinishAttacking()
    {
        isAttacking = false;
    }
    /*public void Punch1()
    {
        
        if(Input.GetMouseButton(0))
        {
            if(!isAttacking)
            {
                StartAttacking();
                animator.SetTrigger("Punch1");
            }
        }
    }*/
    public void TakeDamage()
    {
        // decrease health
        health--;

        // if health is zero or less, die
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

}
