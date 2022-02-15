using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBar : MonoBehaviour
{
    public Transform directionStart;
    public Transform directionEnd;
    public Vector3 direction = Vector3.zero;

    float force = 15;

    // Start is called before the first frame update
    void Start()
    {
        direction = (directionEnd.position - directionStart.position).normalized;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Ball.Player.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Acceleration);
        }
    }
}
