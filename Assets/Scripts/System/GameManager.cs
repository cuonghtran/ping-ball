using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CoreData coreData;
    private readonly string firstPlay = "FirstPlay";
    public readonly string soundTogglePref = "SoundToggle";
    public AudioMixerGroup mainMixerGroup;
    public Texture2D defaultCursor;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitDataOnOpeningGame();
        //SetDefaultCursor();
    }

    void SetDefaultCursor()
    {
        Vector2 cursorOffset = new Vector2(defaultCursor.width / 2, defaultCursor.height / 2);
        Cursor.SetCursor(defaultCursor, cursorOffset, CursorMode.Auto);
    }

    void InitDataOnOpeningGame()
    {
        try
        {
            MusicSettings();
            SaveLoadSystem.LoadGame(coreData);
        }
        catch (FileNotFoundException e)
        {
            InitPlayerData();
            FirstPlayMusicSettings();
        }
    }

    void InitPlayerData()
    {
        coreData.progress = 0;
        coreData.SoundOn = true;
    }

    public void FirstPlayMusicSettings()
    {
        coreData.SoundOn = true;
        mainMixerGroup.audioMixer.SetFloat("Volume", 0);
        PlayerPrefs.SetInt(soundTogglePref, 1);
    }

    public void MusicSettings()
    {
        var soundValue = coreData.SoundOn;
        if (soundValue)
        {
            mainMixerGroup.audioMixer.SetFloat("Volume", 0);
            coreData.SoundOn = true;
        }
        else
        {
            mainMixerGroup.audioMixer.SetFloat("Volume", -80);
            coreData.SoundOn = false;
        }
    }

    public void SetSound(bool soundOn)
    {
        if (soundOn)
        {
            mainMixerGroup.audioMixer.SetFloat("Volume", 0);
            coreData.SoundOn = true;
        }
        else
        {
            mainMixerGroup.audioMixer.SetFloat("Volume", -80);
            coreData.SoundOn = false;
        }
    }
}
