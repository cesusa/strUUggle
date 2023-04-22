using System;
using TMPro;
using UnityEngine;

public class EnemyHeader: MonoBehaviour
{
    public string HeaderMessage;
    public TextMeshProUGUI HeaderText;

    private void Awake()
    {
        HeaderText.text = HeaderMessage;
    }
}