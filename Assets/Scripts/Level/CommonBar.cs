using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBar : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            AudioManager.SharedInstance.Play("Impact_Sound");
        }
    }
}
