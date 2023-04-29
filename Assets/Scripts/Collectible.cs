using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<PlayerPanicAttack>().Happiness++;
        Destroy(gameObject);
    }
}
