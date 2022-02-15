using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBar : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    Vector3 startingPoint;
    bool goingForward = true;
    public float speed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        startingPoint = pointA.position;
        transform.position = startingPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, pointB.position) <= 0.1f && goingForward)
            goingForward = false;

        if (Vector3.Distance(transform.position, pointA.position) <= 0.1f && !goingForward)
            goingForward = true;
    }

    private void FixedUpdate()
    {
        if (goingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }
        else if (!goingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
        }
    }
}
