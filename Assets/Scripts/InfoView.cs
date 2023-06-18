using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class InfoView : MonoBehaviour
{
    public PauseMenu PauseMenu;
    public ThirdPersonController PlayerMv;
    public PlayerPanicAttack Player;
    public PlayerCpnt PlayerCptn;
    
    public GameObject Content;
    public Image Background;

    public bool IsDisabled = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !IsDisabled)
        {
            Content.gameObject.SetActive(!Content.gameObject.activeSelf);
            
            if (Content.gameObject.activeSelf)
            {
                Background.DOFade(0.75f, 0f);
                PauseMenu.Disable();
                Player.Disable();
                PlayerCptn.Disable();
                PlayerMv.IsMoveDisabled = true;
            }
            else
            {
                Background.DOFade(0.0f, 0f);
                PauseMenu.Enable();
                Player.Enable();
                PlayerCptn.Enable();
                PlayerMv.IsMoveDisabled = false;
            }
        }
    }

    public void Disable()
    {
        IsDisabled = true;
    }
}
