using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenUI : MonoBehaviour
{
   
    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void Reload()
    {
        SceneManager.LoadScene(1);
    }

    public void StartScreen()
    {
        SceneManager.LoadScene(0);
    }
}
