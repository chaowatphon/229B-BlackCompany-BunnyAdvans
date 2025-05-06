using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int scoreToWin = 5;

    public Text scoreText;
    public GameObject winPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        if (score >= scoreToWin)
        {
            WinGame();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    void WinGame()
    {
        Debug.Log("You Win!");
        if (winPanel != null)
            winPanel.SetActive(true);

        // เปลี่ยนฉากไปฉากถัดไป หลังจาก 2 วินาที
        Invoke("LoadNextScene", 2f);
    }

    void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // ถ้ายังมีฉากถัดไป ให้โหลด
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("จบเกมแล้ว ไม่มีฉากถัดไป");
            // หรือจะกลับหน้าแรก / ขึ้นข้อความจบเกมก็ได้
        }
    }
}
