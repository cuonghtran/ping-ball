using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MousePointer
{
    public static Vector2 GetScreenPosition(bool useOldSystem = true)
    {
        Vector3 pos = Input.mousePosition;
        return new Vector2(pos.x, pos.y);
    }

    public static Vector2 GetBoundedScreenPosition(bool useOldSystem = true)
    {
        Vector2 rawPos = GetScreenPosition(useOldSystem);
        float x = Mathf.Clamp(rawPos.x, 0, Screen.width - 1);
        float y = Mathf.Clamp(rawPos.y, 0, Screen.height - 1);
        return new Vector2(x, y);
    }

    public static Vector2 GetViewportPosition(bool useOldSystem = true)
    {
        Vector2 screenPos = GetScreenPosition(useOldSystem);
        return screenPos / new Vector2(Screen.width, Screen.height);
    }

    public static Vector3 GetWorldPosition(Camera camera, bool usedOldSystem = true)
    {
        return GetWorldPosition(camera, camera.nearClipPlane, usedOldSystem);
    }

    public static Vector3 GetWorldPosition(Camera camera, float worldDepth, bool useOldSystem = true)
    {
        Vector2 screenPos = GetBoundedScreenPosition(useOldSystem);
        Vector3 screenPosWithDepth = new Vector3(screenPos.x, screenPos.y, worldDepth);
        return camera.ScreenToWorldPoint(screenPosWithDepth);
    }

    public static Ray GetWorldRay(Camera camera, bool useOldSystem = true)
    {
        Vector2 screenPos = GetBoundedScreenPosition(useOldSystem);
        Vector3 screenPosWithDepth = new Vector3(screenPos.x, screenPos.y, camera.nearClipPlane);
        return camera.ScreenPointToRay(screenPosWithDepth);
    }
}
