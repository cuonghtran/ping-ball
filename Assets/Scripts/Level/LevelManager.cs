using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager SharedInstance;
    [Header("Data")]
    public CoreData coreData;

    [Header("Information")]
    public int levelIndex;
    public Camera mainCamera;

    [Header("Mouse Cursor Textures")]
    public Texture2D defaultCursor;
    public Texture2D pointCursor;
    public Texture2D grabCursor;

    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeMouseCursor("default");
    }

    public void SaveProgress()
    {
        if (levelIndex > coreData.progress)
            coreData.progress = levelIndex;

        SaveLoadSystem.SaveGame(coreData);
    }

    public void GoNext()
    {
        int nextLvl = levelIndex + 1;
        ChangeScene("Level" + nextLvl.ToString());
    }

    public void ExitToMenu()
    {
        ChangeScene(ConstantsList.Scenes["OpeningScene"]);
    }

    void ChangeScene(string sceneName)
    {
        SceneController.Instance.FadeAndLoadScene(sceneName);
    }

    public void ChangeMouseCursor(string mouseState)
    {
        Texture2D cursorText;
        if (mouseState == "default")
            cursorText = defaultCursor;
        else if (mouseState == "point")
            cursorText = pointCursor;
        else if (mouseState == "grab")
            cursorText = grabCursor;
        else cursorText = defaultCursor;

        Vector2 cursorOffset = new Vector2(cursorText.width / 2, cursorText.height / 2);
        Cursor.SetCursor(cursorText, cursorOffset, CursorMode.Auto);
    }
}
