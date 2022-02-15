using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Anchor : MonoBehaviour
{
    [Header("Info")]
    public float gravityRadius = 4f;
    public float gravityForce = 8;
    [SerializeField] private Transform targetBall;
    public bool isFinalAnchor;
    public string instructionText;

    [Header("References")]
    public Transform pointA;
    public Transform pointB;
    private Transform glowFX;

    bool isPatrolling;
    bool goingForward = true;
    float patrolSpeed = 1.4f;

    Collider aCollider;
    MeshRenderer meshRenderer;

    private bool isCaptured;
    public bool IsCaptured { get { return isCaptured; } }

    private void Start()
    {
        aCollider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
        glowFX = transform.GetChild(0);
        if (pointA != null && pointB != null)
            isPatrolling = true;
    }

    private void Update()
    {
        if (isPatrolling && !isCaptured)
        {
            if (Vector3.Distance(transform.position, pointB.position) <= 0.1f && goingForward)
                goingForward = false;

            if (Vector3.Distance(transform.position, pointA.position) <= 0.1f && !goingForward)
                goingForward = true;
        }

        if (targetBall != null)
        {
            float sqrMag = (targetBall.position - transform.position).sqrMagnitude;
            // Vector3.Distance(transform.position, targetBall.position) <= 0.25f || 
            if (sqrMag <= 0.13f)
            {
                Ball.Player.HitNewAnchor(transform, isFinalAnchor, instructionText);
                isCaptured = true;
                aCollider.enabled = false;
                meshRenderer.enabled = false;
                targetBall.position = transform.position;
                targetBall = null;
                glowFX.gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isPatrolling && !isCaptured)
        {
            if (goingForward)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointB.position, patrolSpeed * Time.deltaTime);
            }
            else if (!goingForward)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointA.position, patrolSpeed * Time.deltaTime);
            }
        }

            if (targetBall != null && targetBall.position != transform.position)
        {
            if (!Ball.Player.IsHit)
            {
                Vector3 direction = (transform.position - targetBall.position).normalized;
                targetBall.GetComponent<Rigidbody>().AddForce(direction * gravityForce);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetBall = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetBall = null;
            aCollider.enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, gravityRadius);
    }
}
