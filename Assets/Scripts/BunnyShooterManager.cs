using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyShooterManager : MonoBehaviour
{
    public Transform shootPoint;
    public LineRenderer lineRenderer;
    public GameObject bunnyPrefab;
    public float forceMultiplier = 5f;
    public int trajectoryPoints = 30;
    public float timeStep = 0.1f;

    private Camera cam;
    private Vector2 dragStartPos;
    private bool isDragging = false;
    private GameObject currentBunny;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = cam.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 dragCurrentPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 forceDir = (dragStartPos - dragCurrentPos); // 🔁 เปลี่ยนทิศทาง
            ShowTrajectory(shootPoint.position, forceDir * forceMultiplier);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Vector2 dragReleasePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 forceDir = (dragStartPos - dragReleasePos); // 🔁 เปลี่ยนทิศทาง
            ShootBunny(forceDir * forceMultiplier);
            isDragging = false;
            lineRenderer.enabled = false;
        }
    }

    void ShowTrajectory(Vector2 startPos, Vector2 initialVelocity)
    {
        lineRenderer.positionCount = trajectoryPoints;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * timeStep;
            Vector2 point = startPos + initialVelocity * t + 0.5f * Physics2D.gravity * t * t;
            lineRenderer.SetPosition(i, point);
        }

        lineRenderer.enabled = true;
    }

    void ShootBunny(Vector2 force)
    {
        if (currentBunny == null)
        {
            Debug.LogWarning("currentBunny is null! Make sure SpawnBunny() was called in Start().");
            return;
        }

        Rigidbody2D rb = currentBunny.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.rotation = 0;

        currentBunny.transform.position = shootPoint.position;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    void SpawnBunny()
    {
        currentBunny = Instantiate(bunnyPrefab, shootPoint.position, Quaternion.identity);
    }
    
}
