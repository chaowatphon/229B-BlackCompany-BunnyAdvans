using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyShooter : MonoBehaviour
{
     public GameObject bunny;
    public Transform spawnPoint;
    public LineRenderer lineRenderer;
    public float forceMultiplier = 5f;

    private Rigidbody2D bunnyRb;
    private Vector2 dragStartPos;
    private bool isDragging = false;
    private bool isBunnyFlying = false;

    private float idleTimer = 0f;
    private float idleThreshold = 0.05f; // ความเร็วต่ำสุดที่ถือว่าหยุด
    private float timeBeforeReset = 3f; // เวลาที่จะรอก่อนรีเซ็ต
    
    void Start()
    {
        bunnyRb = bunny.GetComponent<Rigidbody2D>();
        ResetBunny();
    }

    void Update()
    {
        if (isBunnyFlying)
        {
            float speed = bunnyRb.velocity.magnitude;

            if (speed < idleThreshold)
            {
                idleTimer += Time.deltaTime;

                if (idleTimer >= timeBeforeReset)
                {
                    ResetBunny();
                    return;
                }
            }
            else
            {
                idleTimer = 0f; // ถ้ายังเคลื่อนที่อยู่ รีเซ็ตตัวจับเวลา
            }

            return; // หยุดการ input ถ้ากำลังบิน
        }

        // --- Input คลิกยิง ---
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = dragStartPos - currentPos;
            ShowTrajectory(direction);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Vector2 releasePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = dragStartPos - releasePos;

            bunnyRb.bodyType = RigidbodyType2D.Dynamic;
            bunnyRb.gravityScale = 1.5f;
            bunnyRb.velocity = Vector2.zero;
            bunnyRb.angularVelocity = 0f;
            bunnyRb.AddForce(direction * forceMultiplier, ForceMode2D.Impulse);

            isDragging = false;
            isBunnyFlying = true;
            lineRenderer.positionCount = 0;
            idleTimer = 0f; // เริ่มจับเวลาหลังจากยิง
        }
    }

    void ResetBunny()
    {
        bunny.transform.position = spawnPoint.position;
        bunny.transform.rotation = Quaternion.identity; // 💡 รีเซ็ตให้หันตรงเหมือนเดิม

        bunnyRb.velocity = Vector2.zero;
        bunnyRb.angularVelocity = 0f;
        bunnyRb.bodyType = RigidbodyType2D.Kinematic;
        bunnyRb.gravityScale = 0f;

        isBunnyFlying = false;
        idleTimer = 0f;
    }

    void ShowTrajectory(Vector2 forceDirection)
    {
        int numPoints = 20;
        float timeStep = 0.1f;
        Vector2 startPosition = bunny.transform.position;
        Vector2 startVelocity = forceDirection * forceMultiplier / bunnyRb.mass;

        lineRenderer.positionCount = numPoints;
        for (int i = 0; i < numPoints; i++)
        {
            float t = i * timeStep;
            Vector2 pos = startPosition + startVelocity * t + 0.5f * Physics2D.gravity * t * t;
            lineRenderer.SetPosition(i, pos);
        }
    }
}
