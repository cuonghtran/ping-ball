using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBar : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (!Ball.Player.IsHit)
            {
                StartCoroutine(Ball.Player.DeathSequence());
            }
        }
    }
}
