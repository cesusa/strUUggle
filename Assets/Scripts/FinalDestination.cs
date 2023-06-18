using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FinalDestination : MonoBehaviour
{
    public PauseMenu PauseMenu;
    public InfoView InfoView;
    
    public GameObject Content;
    public GameObject ProgressContent;
    public RectTransform ButtonsLayout;
    public Image Background;
    public Button RestartButton;
    public Button NextLevelButton;
    public Button MainMenuButton;
    public Button ExitButton;
    public TextMeshProUGUI ScorePercentageText;
    public TextMeshProUGUI ScoreValueText;
    public TextMeshProUGUI ResultText;
    public Image ScoreImage;

    public string MainMenu;
    public string NextLevel;
    public string WinText;
    public string LoseText;
    public float EveryValueDuration = 0.05f;

    private void Start()
    {
        RestartButton.onClick.AddListener(RestartButtonClick);
        NextLevelButton.onClick.AddListener(NextLevelClick);
        MainMenuButton.onClick.AddListener(MainMenuClick);
        ExitButton.onClick.AddListener(Application.Quit);
    }

    private void OnDisable()
    {
        RestartButton.onClick.RemoveListener(RestartButtonClick);
        NextLevelButton.onClick.RemoveListener(NextLevelClick);
        MainMenuButton.onClick.RemoveListener(MainMenuClick);
        ExitButton.onClick.RemoveListener(Application.Quit);
    }

    public void RestartButtonClick()
    {
        Debug.Log($"Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevelClick()
    {
        SceneManager.LoadScene(NextLevel);
    }

    public void MainMenuClick()
    {
        SceneManager.LoadScene(MainMenu);
    }

    private void OnTriggerEnter(Collider other)
    {
        Content.gameObject.SetActive(true);
        var player = other.GetComponent<PlayerPanicAttack>();
        var playerCptn = other.GetComponent<PlayerCpnt>();
        var playerMv = other.GetComponent<ThirdPersonController>();
        var isPlayerValidTiFinishLevel = ((float)player.Happiness / player.MaxHappiness) >= 0.5f;
        
        Reset();
        PauseMenu.Disable();
        player.Disable();
        playerCptn.Disable();
        playerMv.IsMoveDisabled = true;
        InfoView.Disable();

        ScorePercentageText.SetText($"0%");
        ScoreValueText.SetText($"{0}/{player.MaxHappiness}");
        
        Background.DOFade(0.75f, 0.25f);
        
        Sequence sequence = DOTween.Sequence();
        Button requiredButton = null;
        if (isPlayerValidTiFinishLevel)
        {
            if (!string.IsNullOrEmpty(NextLevel))
            {
                requiredButton = NextLevelButton;
            }
        }
        else
        {
            requiredButton = RestartButton;
        }
        
        ResultText.SetText(isPlayerValidTiFinishLevel ? WinText : LoseText);

        sequence.Append(ProgressContent.transform.DOScale(Vector3.one, 0.25f));

        sequence.Append(ProgressContent.transform.DOScale(Vector3.one, 0.25f));

        sequence.Append(DOVirtual.Float(0, player.Happiness, player.Happiness * EveryValueDuration, value =>
        {
            ScoreImage.fillAmount = value / player.MaxHappiness;
            ScorePercentageText.SetText($"{((value/player.MaxHappiness) * 100).ToString("00.0")}%");
            ScoreValueText.SetText($"{(int)value}/{player.MaxHappiness}");
        }).SetEase(Ease.Linear));

        sequence.Append(ResultText.transform.DOScale(Vector3.one, 0.25f));

        if (requiredButton != null)
        {
            sequence.Append(requiredButton.transform.DOScale(Vector3.one, 0.25f).OnStart(() => requiredButton.gameObject.SetActive(true)).OnUpdate(() => LayoutRebuilder.ForceRebuildLayoutImmediate(ButtonsLayout)));
        }
        
        sequence.Append(MainMenuButton.transform.DOScale(Vector3.one, 0.25f)).OnStart(() => MainMenuButton.gameObject.SetActive(true)).OnUpdate(() => LayoutRebuilder.ForceRebuildLayoutImmediate(ButtonsLayout));
        sequence.Append(ExitButton.transform.DOScale(Vector3.one, 0.25f)).OnStart(() => ExitButton.gameObject.SetActive(true)).OnUpdate(() => LayoutRebuilder.ForceRebuildLayoutImmediate(ButtonsLayout));
    }

    private void Reset()
    {
        Background.DOFade(0f, 0f);
        ProgressContent.transform.DOScale(0, 0f);
        ResultText.transform.DOScale(0, 0f);
        NextLevelButton.transform.DOScale(0, 0f);
        RestartButton.transform.DOScale(0, 0f);
        MainMenuButton.transform.DOScale(0, 0f);
        ExitButton.transform.DOScale(0, 0f);
    }
}
