using System;
using UnityEngine;

public class CureArea: MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<PlayerPanicAttack>().IsPlayerInCureArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<PlayerPanicAttack>().IsPlayerInCureArea = false;
    }
}