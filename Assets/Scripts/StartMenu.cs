using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        // โหลดด่านแรก 
        SceneManager.LoadScene("Map1");
    }
}
