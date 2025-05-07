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
        string[] sceneOrder = { "Map1", "Map2", "Map3", "EndingScene", "CreditScene" };
        string currentScene = SceneManager.GetActiveScene().name;

        int currentIndex = System.Array.IndexOf(sceneOrder, currentScene);

        if (currentIndex >= 0 && currentIndex < sceneOrder.Length - 1)
        {
            string nextScene = sceneOrder[currentIndex + 1];
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            // จบเกมหรือไม่พบฉากถัดไป → กลับไปหน้า Start
            SceneManager.LoadScene("StartScene");
        }
    }
    
}
