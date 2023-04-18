using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemai : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;
    public float attackDistance = 1.0f;
    public int health = 100;

    private Vector3 currentTarget;
    private bool movingRight = true;
    private bool playerDetected = false;
    private Vector3 playerPosition;

    void Start()
    {
        currentTarget = pointA.position;
    }

    void Update()
    {
        if (playerDetected)
        {
            if (Vector3.Distance(transform.position, playerPosition) <= attackDistance)
            {
                // Saldýrý yap
            }
            else
            {
                currentTarget = playerPosition;
            }
        }

        if (transform.position == currentTarget)
        {
            if (currentTarget == pointA.position)
            {
                currentTarget = pointB.position;
            }
            else
            {
                currentTarget = pointA.position;
            }

            Flip();
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }

    void Flip()
    {
        movingRight = !movingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
            playerPosition = other.gameObject.transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
}
