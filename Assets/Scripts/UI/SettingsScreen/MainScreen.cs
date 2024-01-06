using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрылась!");
        Application.Quit();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Time.timeScale = 1f;
    }

    public void SaveSettings()
    {
        
    }

    public void Vibration()
    {
        Handheld.Vibrate();
    }

    public void Message()
    {
        
    }
}
