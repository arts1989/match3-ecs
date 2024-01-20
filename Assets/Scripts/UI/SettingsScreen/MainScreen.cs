using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    [SerializeField] private SettingWindow _settingWindow;

public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void ExitGame()
    {
        Debug.Log("���� ���������!");
        Application.Quit();
    }

    public void OpenSettings()
    {
        Instantiate(_settingWindow.gameObject, transform.parent);
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
