using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TutorialArea: MonoBehaviour
{
    public string TutorialMessage;
    [Range(0.05f, 1f)] public float WriteSpeed = .10f;

    private Tween _typeWriterTween;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        TutorialView.Instance.OnTextTriggerEnter(TutorialMessage, WriteSpeed);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        TutorialView.Instance.OnTextTriggerExit();
    }
}