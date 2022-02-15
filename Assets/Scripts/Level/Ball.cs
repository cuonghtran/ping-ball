using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball Player;

    [Header("References")]
    public Transform currentAnchor;
    public ParticleSystem explosionFX;
    public TrailRenderer trailFX;
    public ParticleSystem rainbowFX;
    public float maxSpeed = 2f;

    [Header("Trajectory")]
    public GameObject pointPrefab;
    public int numberOfPoints;
    private GameObject[] trajectoryPoints;

    Rigidbody rb;
    MeshRenderer meshRenderer;
    Camera mCamera;
    Vector3 direction;
    float normalizedPull;
    float maxPull = 1f;
    Vector3 mOffset;
    float mZCoord;
    bool isMoving;
    bool isHit;
    public bool IsHit { get { return isHit; } }
    float selfDestructDuration = 7f;
    float selfDestructTimer = 0;

    private void Awake()
    {
        Player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        mCamera = LevelManager.SharedInstance.mainCamera;

        trajectoryPoints = new GameObject[numberOfPoints];

        for (int i = 0; i < trajectoryPoints.Length; i++)
        {
            trajectoryPoints[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            selfDestructTimer += Time.deltaTime;
            if (selfDestructTimer >= selfDestructDuration)
                StartCoroutine(DeathSequence());
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
            CameraController.Instance.SetPosition(transform.position, currentAnchor.position);
    }

    private Vector3 PointPosition(float time, Vector3 force)
    {
        return transform.position + (force * time) + 0.5f * Physics.gravity * Mathf.Pow(time, 2);
    }

    void OnMouseDown()
    {
        if (UIManager.Instance.GamePaused)
            return;

        // drag handler
        if (!isMoving)
        {
            selfDestructTimer = 0;
            mZCoord = mCamera.WorldToScreenPoint(currentAnchor.position).z;
            mOffset = currentAnchor.position - GetMouseAsWorldPoint();
        }

        LevelManager.SharedInstance.ChangeMouseCursor("grab");
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        // Convert it to world points
        return mCamera.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if (UIManager.Instance.GamePaused)
            return;

        if (!isMoving)
        {
            rb.isKinematic = true;

            direction = (GetMouseAsWorldPoint() - currentAnchor.transform.position).normalized;
            float pull = Vector3.Distance(GetMouseAsWorldPoint(), currentAnchor.transform.position);
            normalizedPull = Mathf.Clamp(pull, 0f, maxPull);

            transform.position = currentAnchor.transform.position + (direction * normalizedPull) + mOffset;

            for (int i = 0; i < trajectoryPoints.Length; i++)
            {
                trajectoryPoints[i].transform.position = PointPosition(i * 0.04f, -direction * normalizedPull * maxSpeed);
            }
        }
    }

    private void OnMouseUp()
    {
        if (UIManager.Instance.GamePaused)
            return;

        if (!isMoving)
        {
            AudioManager.SharedInstance.Play("Fire_Sound");
            rb.isKinematic = false;
            SetBallMoving(true);
            trailFX.emitting = true;
            rb.velocity = -direction * normalizedPull * maxSpeed;

            foreach(GameObject p in trajectoryPoints)
            {
                p.transform.position = currentAnchor.position;
            }
        }

        LevelManager.SharedInstance.ChangeMouseCursor("default");
    }

    public void HitNewAnchor(Transform newAnchor, bool isFinalAnchor, string insText="")
    {
        AudioManager.SharedInstance.Play("Hit_Sound");
        // add FXs
        rainbowFX.Play();

        rb.isKinematic = true;
        currentAnchor = newAnchor;
        CameraController.Instance.ResetPosition(newAnchor.position);
        SetBallMoving(false);

        if (insText != "")
        {
            StartCoroutine(UIManager.Instance.DisplayInstructions(insText));
        }

        if (isFinalAnchor) // end level
        {
            UIManager.Instance.OpenFinishMenu();
        }
    }

    public IEnumerator DeathSequence()
    {
        AudioManager.SharedInstance.Play("Explosion_Sound_1");
        GameEvents.Current.ResetButton();

        SetBallMoving(false);
        trailFX.emitting = false;
        meshRenderer.enabled = false;
        rb.velocity = Vector3.zero;
        PlayExplosionFX();

        yield return new WaitForSeconds(0.45f);

        ResetAfterDeath();
    }

    void ResetAfterDeath()
    {
        transform.position = currentAnchor.position;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        meshRenderer.enabled = true;
        rb.isKinematic = true;
        isHit = false;

        CameraController.Instance.ResetPosition(transform.position);
    }

    public void SetBallMoving(bool state)
    {
        isMoving = state;
    }

    public void PlayExplosionFX()
    {
        if (!isHit)
        {
            explosionFX.Play();
            isHit = true;
        }
    }
}
