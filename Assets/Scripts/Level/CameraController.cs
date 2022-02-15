using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public float topBound;
    Vector3 oldPos;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetPosition(Vector3 newLocation, Vector3 currentAnchorPos)
    {
        if (currentAnchorPos.y + 6 < newLocation.y)
        {
            LeanTween.moveLocalY(gameObject, Mathf.Min(topBound, newLocation.y - 2f), 0.6f);
        }

        if (currentAnchorPos.y - 2.8f > newLocation.y)
        {
            LeanTween.moveLocalY(gameObject, Mathf.Min(topBound, newLocation.y + 1.5f), 0.6f);
        }
    }

    public void ResetPosition(Vector3 newLocation)
    {
        LeanTween.moveLocalY(gameObject, Mathf.Min(topBound, newLocation.y + 2f), 0.6f);
    }

    //public void SetAnchorPosition(Vector3 newLocation)
    //{
    //    if (newLocation.y + 2 > transform.position.y)
    //        LeanTween.moveLocalY(gameObject, Mathf.Min(topBound, newLocation.y + 2f), 0.6f);
    //}
}
