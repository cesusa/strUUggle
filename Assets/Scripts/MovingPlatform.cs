using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;

    private Vector3 currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        currentTarget = pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == currentTarget)
        {
            if (currentTarget == pointA.position)
            {
                currentTarget = pointB.position;
            }
            else
            {
                currentTarget = pointA.position;
            }

            
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }
}
