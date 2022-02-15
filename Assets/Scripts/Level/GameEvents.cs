using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Current;

    private void Awake()
    {
        if (Current == null)
            Current = this;
    }

    public event Action onResetButton;
    public void ResetButton()
    {
        if (onResetButton != null)
            onResetButton.Invoke();
    }
}
