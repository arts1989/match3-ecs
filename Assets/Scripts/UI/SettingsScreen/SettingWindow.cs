using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingWindow : MonoBehaviour
{
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
