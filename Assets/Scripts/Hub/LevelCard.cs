using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LevelCard : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.SharedInstance.Play("UIButton_Sound");
        string selectedLevel = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        GeneralUI.Instance.PlayLevel(selectedLevel.Trim());
    }
}
