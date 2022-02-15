using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectHandler : MonoBehaviour
{
    [Header("Data")]
    public CoreData coreData;

    public Transform levelPanel;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevelProgress();
    }

    void LoadLevelProgress()
    {
        foreach (Transform levelCard in levelPanel)
        {
            TextMeshProUGUI lvlText = levelCard.GetChild(0).GetComponent<TextMeshProUGUI>();
            if (int.Parse(lvlText.text) <= coreData.progress + 1)
            {
                LeanTween.alphaCanvas(levelCard.GetComponent<CanvasGroup>(), 1, 0);
            }
            else
            {
                LeanTween.alphaCanvas(levelCard.GetComponent<CanvasGroup>(), 0.3f, 0);
                levelCard.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }
}
