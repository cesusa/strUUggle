using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanicAttack: MonoBehaviour
{
    public GameObject PanicAttackPressFContent;
    public Image PanicAttackEffectImage;
    public Image StressImage;
    public Image HappinessImage;
    public AudioClip SirenAudio;
    public AudioSource AudioPlayer;
    public TextMeshProUGUI HappinessText;
    
    public KeyCode ReleasePanicAttackKey = KeyCode.F;
    public int MinSecForPanicAttack = 45;
    public int MaxSecForPanicAttack = 60;
    public float RequiredTimeForCure = 4f;
    public float PanicAttackRedEffectDuration = 0.5f;
    public int MaxStress = 100;
    public int MaxHappiness = 100;
    public float HappinessReductionInterval = 1f;
    public float StressIncreaseInterval = 1f;

    private float _cureRoutineYieldTime = 0.1f;
    private int _happiness = 0;
    private int _stress = 0;
    private bool _isPanicAttacking = false;
    private bool _isPlayerInCureArea = false;
    private Coroutine _panicAttackCureRoutine;

    private Sequence _panicAttackEffectImageFadeSequence;
    private Sequence _happinessSequence;
    private Sequence _stressSequence;

    public event Action<bool> OnPanicAttackChanged;

    private IDisposable _stressIntervalDisposable;

    private void Start()
    {
        StartCoroutine(StartPanicAttack());

        GetComponent<PlayerCpnt>()
            .ObserveEveryValueChanged(player => player.isAttacking)
            .Subscribe(isAttacking =>
            {
                if (!isAttacking)
                {
                    _stressIntervalDisposable = Observable
                        .Interval(TimeSpan.FromSeconds(1))
                        .Subscribe(_ =>
                        {
                            Stress -= 2;
                        });
                }
                else
                {
                    Stress++;
                    _stressIntervalDisposable?.Dispose();
                }
            });
    }

    public void Update()
    {
        if (!IsPanicAttacking && !IsPlayerInCureArea)
        {
            return;
        }
        
        if (Input.GetKeyDown(ReleasePanicAttackKey))
        {
            Debug.Log($"Key Down ReleasePanicAttackKey");
            _panicAttackCureRoutine = StartCoroutine(StartPanicAttackCure());
        }

        if (Input.GetKeyUp(ReleasePanicAttackKey))
        {
            Debug.Log($"Key Up ReleasePanicAttackKey");
            StopPanicAttackCure();
        }
    }

    private IEnumerator StartPanicAttackCure()
    {
        var cureTime = 0f;
        
        while (cureTime < RequiredTimeForCure)
        {
            Debug.Log($"cureTime {cureTime}");
            
            yield return new WaitForSeconds(_cureRoutineYieldTime);
            cureTime += _cureRoutineYieldTime;
        }

        Cure();
    }

    private void StopPanicAttackCure()
    {
        if (_panicAttackCureRoutine == null) return;
        
        StopCoroutine(_panicAttackCureRoutine);
        _panicAttackCureRoutine = null;
        
        Debug.Log($"StopPanicAttackCure");
    }

    private void Cure()
    {
        IsPanicAttacking = false;

        _panicAttackEffectImageFadeSequence?.Kill();
        _panicAttackEffectImageFadeSequence = null;
        PanicAttackEffectImage.DOFade(0, 0);
        PanicAttackEffectImage.gameObject.SetActive(false);
        PanicAttackPressFContent.SetActive(false);
        AudioPlayer.Stop();

        _happinessSequence?.Kill();
        _stressSequence?.Kill();

        Stress = 0;
        
        // DOVirtual

        StartCoroutine(StartPanicAttack());
    }

    private IEnumerator StartPanicAttack()
    {
        var duration = UnityEngine.Random.Range(MinSecForPanicAttack, MaxSecForPanicAttack);
        Debug.Log($"Duration for next panic attack: {duration}");
        
        yield return new WaitForSeconds(duration);

        PanicAttack();
    }

    private void PanicAttack()
    {
        IsPanicAttacking = true;
        PanicAttackEffectImage.gameObject.SetActive(true);
        PanicAttackPressFContent.SetActive(true);

        _panicAttackEffectImageFadeSequence = DOTween.Sequence();
        _panicAttackEffectImageFadeSequence.Append(PanicAttackEffectImage.DOFade(0.2f, PanicAttackRedEffectDuration));
        _panicAttackEffectImageFadeSequence.Append(PanicAttackEffectImage.DOFade(0, PanicAttackRedEffectDuration));
        _panicAttackEffectImageFadeSequence.SetLoops(-1);
        _panicAttackEffectImageFadeSequence.Play();

        // _happinessSequence = DOTween.Sequence().AppendInterval(HappinessReductionInterval).OnStepComplete(() => Happiness--).SetLoops(-1);
        _stressSequence = DOTween.Sequence().AppendInterval(StressIncreaseInterval).OnStepComplete(() => Stress++).SetLoops(-1);

        AudioPlayer.clip = SirenAudio;
        AudioPlayer.loop = true;
        AudioPlayer.Play();
    }

    public bool IsPanicAttacking
    {
        get => _isPanicAttacking;
        set
        {
            if (_isPanicAttacking == value) return;
            
            _isPanicAttacking = value;
            
            Debug.Log($"Is Panic Attacking: {value}");

            OnPanicAttackChanged?.Invoke(value);
        }
    }

    public bool IsPlayerInCureArea
    {
        get => _isPlayerInCureArea;
        set
        {
            _isPlayerInCureArea = value;
            
            Debug.Log($"Is Player In Cure Area: {value}");
        }
    }

    public int Happiness
    {
        get => _happiness;
        set
        {
            _happiness = Mathf.Clamp(value, 0, MaxHappiness);

            HappinessText.text = $"Happiness: {_happiness}";

            HappinessImage.DOFillAmount((float)_happiness / MaxHappiness, 0.5f);
        }
    }

    public int Stress
    {
        get => _stress;
        set
        {
            _stress = Mathf.Clamp(value, 0, MaxStress);

            StressImage.DOFillAmount((float)_stress / MaxStress, 0.5f);
        }
    }
}