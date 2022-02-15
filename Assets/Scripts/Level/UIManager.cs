using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public bool GamePaused = false;
    public Transform menuPanel;
    CanvasGroup menuPanelCG;
    public TextMeshProUGUI titleText;
    public Transform pauseButton;
    public GameObject resumeButton;
    public GameObject nextLevelButton;
    public Image SoundButtonImage;
    public Sprite soundOnImage;
    public Sprite soundOffImage;
    public TextMeshProUGUI instructionText;
    bool soundOn;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        menuPanelCG = menuPanel.GetComponent<CanvasGroup>();
    }

    public void OnButtonMouseHover()
    {
        LevelManager.SharedInstance.ChangeMouseCursor("point");
    }

    public void OnButtonMouseExit()
    {
        LevelManager.SharedInstance.ChangeMouseCursor("default");
    }

    void OpenPauseMenu(bool finishMenu=false)
    {
        LevelManager.SharedInstance.ChangeMouseCursor("point");
        GamePaused = true;
        pauseButton.gameObject.SetActive(false);
        menuPanelCG.alpha = 1;
        menuPanelCG.blocksRaycasts = true;

        if (finishMenu)
        {
            LevelManager.SharedInstance.SaveProgress();
            if (LevelManager.SharedInstance.levelIndex == 16)
            {
                titleText.text = "YOU BEAT THE GAME!";
                nextLevelButton.SetActive(false);
            }
            else
            {
                titleText.text = "COMPLETED!";
                nextLevelButton.SetActive(true);
            }
            resumeButton.SetActive(false);
        }
        else
        {
            titleText.text = "PAUSED";
            nextLevelButton.SetActive(false);
            resumeButton.SetActive(true);
        }
    }

    public void OpenFinishMenu()
    {
        AudioManager.SharedInstance.Play("Victory_Sound");
        OpenPauseMenu(true);
    }

    public void OnPauseButton_Click()
    {
        AudioManager.SharedInstance.Play("UIButton_Sound");
        OpenPauseMenu();
    }

    public void OnResumeButton_Click()
    {
        AudioManager.SharedInstance.Play("UIButton_Sound");
        LevelManager.SharedInstance.ChangeMouseCursor("default");
        GamePaused = false;
        pauseButton.gameObject.SetActive(true);
        menuPanelCG.alpha = 0;
        menuPanelCG.blocksRaycasts = false;
    }

    public void OnNextLevelButton_Click()
    {
        AudioManager.SharedInstance.Play("UIButton_Sound");
        LevelManager.SharedInstance.GoNext();
    }

    public void OnExitToMenuButton_Click()
    {
        AudioManager.SharedInstance.Play("UIButton_Sound");
        LevelManager.SharedInstance.ExitToMenu();
    }

    public void ToggleSound()
    {
        soundOn = !soundOn;
        if (soundOn)
            SoundButtonImage.sprite = soundOnImage;
        else SoundButtonImage.sprite = soundOffImage;

        GameManager.Instance.SetSound(soundOn);
    }

    public IEnumerator DisplayInstructions(string insText)
    {
        instructionText.text = insText;
        CanvasGroup instructionCG = instructionText.GetComponent<CanvasGroup>();
        LeanTween.alphaCanvas(instructionCG, 1, 0.75f);
        yield return new WaitForSeconds(4.25f);
        LeanTween.alphaCanvas(instructionCG, 0, 0.75f);
    }
}
