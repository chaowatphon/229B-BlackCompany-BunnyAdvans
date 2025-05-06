using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // อย่าลืมตั้ง tag ของกระต่ายเป็น "Player"
        {
            GameManager.instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}
