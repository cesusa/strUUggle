using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCpnt : MonoBehaviour
{
    public bool isAttacking;
    private Animator animator;
    [SerializeField] private int _health;

    public HealthBar healthbar;

    public int Health
    {
        get => _health;
        set
        {
            if (_health == value) return;

            _health = value;
            OnPlayerHealthChange?.Invoke(_health, value);
        }
    }

    public Action<int, int> OnPlayerHealthChange;

    void Start()
    {
        animator = GetComponent<Animator>();
        isAttacking = false;
        healthbar.SetMaxHealth(Health);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isAttacking)
            {
                isAttacking = true;
                animator.SetTrigger("Punch1");
                
            }
        }
       /* if (Input.GetMouseButton(1))
        {
            if (!isAttacking)
            {
                
                animator.SetTrigger("Kick");
                
            }
        }*/


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Health")
        {
            Health += 5;
           
            healthbar.SetHealth(Health);
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
    public void TakeDamage(int damage)
    {
        // decrease health
        Health -= damage;
        healthbar.SetHealth(Health);

        // if health is zero or less, die
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

}
