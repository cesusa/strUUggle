using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCpnt : MonoBehaviour
{
    public bool isAttacking;
    private Animator animator;
    [SerializeField] private int _health;

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
    }

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
    public void TakeDamage(int damage)
    {
        // decrease health
        Health -= damage;

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
