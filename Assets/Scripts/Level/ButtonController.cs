using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Anchor attachedAnchor;
    public Transform targetDoor;
    public string buttonPushedDirection;

    bool isButtonPushed;
    float openDoorTime = 0.25f;
    float buttonPushedCd = 0.75f;
    float canPush = 0;

    Vector3 buttonStartingPosition;

    private void Start()
    {
        GameEvents.Current.onResetButton += ResetButtonAndDoor;

        buttonStartingPosition = transform.position;
    }

    private void Update()
    {

        if (!isButtonPushed)
            ResetButtonAndDoor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && Time.timeSinceLevelLoad >= canPush && !isButtonPushed)
        {
            AudioManager.SharedInstance.Play("Button_Sound");
            Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
            
            canPush = Time.timeSinceLevelLoad + buttonPushedCd;
            isButtonPushed = true;
            StartCoroutine(PushButton());
            OpenDoor();
        }
    }

    IEnumerator PushButton()
    {
        Vector3 direction = GetDirectionFromString(buttonPushedDirection);
        float elapsed = 0;
        while (elapsed <= openDoorTime)
        {
            elapsed += Time.deltaTime;
            transform.position = transform.position + (direction * 0.015f);
            yield return null;
        }
    }

    void OpenDoor()
    {
        targetDoor.gameObject.SetActive(false);
    }

    Vector3 GetDirectionFromString(string dir)
    {
        switch(dir.ToLower())
        {
            case "up":
                return Vector3.up;
            case "down":
                return Vector3.down;
            case "right":
                return Vector3.right;
            case "left":
                return Vector3.left;
            default: return Vector3.down;
        }
    }

    void ResetButtonAndDoor()
    {
        if (!attachedAnchor.IsCaptured)
        {
            isButtonPushed = false;
            transform.position = buttonStartingPosition;
            targetDoor.gameObject.SetActive(true);
        }
    }
}
