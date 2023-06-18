using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MissionView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject MissionContent;
    public TextMeshProUGUI MissionText;

    public string Message;

    public void OnPointerEnter(PointerEventData eventData)
    {
        MissionContent.gameObject.SetActive(true);
        MissionText.SetText(Message);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MissionContent.gameObject.SetActive(false);
    }
}
