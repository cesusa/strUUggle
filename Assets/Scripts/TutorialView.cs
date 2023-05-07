using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TutorialView: MonoBehaviour
{
    public static TutorialView Instance;

    [SerializeField] private GameObject _tutorialContent;
    [SerializeField] private TextMeshProUGUI _tutorialMessageText;
    
    private Tween _typeWriterTween;

    private void Awake()
    {
        Instance = this;
    }

    public void OnTextTriggerEnter(string tutorialMessage, float writeSpeed)
    {
        var message = string.Empty;
        
        _tutorialContent.SetActive(true);
        _typeWriterTween = DOTween.To(() => message, x => message = x, tutorialMessage, tutorialMessage.Length * writeSpeed)
            .OnUpdate(() =>
            {
                _tutorialMessageText.SetText(message);
            }).SetEase(Ease.Linear);

        _tutorialMessageText.DOFade(1, 0.5f);
    }

    public void OnTextTriggerExit()
    {
        _tutorialMessageText.DOFade(0, 0.5f).OnComplete(() =>
        {
            _tutorialContent.SetActive(false);
            _tutorialMessageText.text = string.Empty;
            _typeWriterTween?.Kill();
        });
    }
}