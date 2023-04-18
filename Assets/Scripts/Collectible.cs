using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        CollectibleController.coinCount += 1;
        Destroy(gameObject);
    }
}
