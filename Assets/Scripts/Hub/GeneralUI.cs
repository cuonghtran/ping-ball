using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUI : MonoBehaviour
{
    public static GeneralUI Instance;

    [Header("UI")]
    public Transform openingPanel;
    public Transform levelSelectPanel;
    CanvasGroup openingPanelCG;
    CanvasGroup levelSelectPanelCG;

    public Image SoundButtonImage;
    public Sprite soundOnImage;
    public Sprite soundOffImage;
    bool soundOn;

    string selectedLevel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        openingPanelCG = openingPanel.GetComponent<CanvasGroup>();
        levelSelectPanelCG = levelSelectPanel.GetComponent<CanvasGroup>();

        Invoke(nameof(Theme), 0.75f);
    }

    void Theme()
    {
        AudioManager.SharedInstance.PlayTheme("Main_Theme");
    }

    public void OnPlayButton_Click()
    {
        AudioManager.SharedInstance.Play("UIButton_Sound");
        LeanTween.alphaCanvas(openingPanelCG, 0, 0.25f).setOnComplete(DisplayLevels);
    }

    void DisplayLevels()
    {
        openingPanelCG.blocksRaycasts = false;
        LeanTween.alphaCanvas(levelSelectPanelCG, 1, 0.25f).setOnComplete(() => levelSelectPanelCG.blocksRaycasts = true);
    }

    public void ToggleSound()
    {
        soundOn = !soundOn;
        if (soundOn)
            SoundButtonImage.sprite = soundOnImage;
        else SoundButtonImage.sprite = soundOffImage;

        GameManager.Instance.SetSound(soundOn);
    }

    public void PlayLevel(string levelName)
    {
        selectedLevel = levelName;
        SceneController.Instance.FadeAndLoadScene(ConstantsList.Scenes[selectedLevel]);
    }
}
