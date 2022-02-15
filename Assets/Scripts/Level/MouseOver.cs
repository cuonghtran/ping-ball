using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnMouseExit();
    }

    private void OnMouseOver()
    {
        LevelManager.SharedInstance.ChangeMouseCursor("over");
    }

    private void OnMouseExit()
    {
        LevelManager.SharedInstance.ChangeMouseCursor("over");
    }
}
