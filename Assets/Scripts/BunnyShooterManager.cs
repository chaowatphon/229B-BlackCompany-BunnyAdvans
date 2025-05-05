using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyShooterManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform shootPoint; // จุดยิง (ต้นทางของกระต่าย)
    public float forceMultiplier = 10f;
    public int lineSegmentCount = 30;
    public float timeStep = 0.1f;

    private Camera cam;
    private bool isDragging;

    void Start()
    {
        cam = Camera.main;
        lineRenderer.positionCount = lineSegmentCount;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            lineRenderer.enabled = false;
        }

        if (isDragging)
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (Vector2)(mousePos - shootPoint.position);
            direction.Normalize();

            ShowTrajectory(shootPoint.position, direction * forceMultiplier);
        }
    }

    void ShowTrajectory(Vector2 startPos, Vector2 initialVelocity)
    {
        lineRenderer.enabled = true;

        Vector2 gravity = Physics2D.gravity;
        for (int i = 0; i < lineSegmentCount; i++)
        {
            float t = i * timeStep;
            Vector2 point = startPos + initialVelocity * t + 0.5f * gravity * t * t;
            lineRenderer.SetPosition(i, point);
        }
    }
}
