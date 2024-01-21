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
        Debug.Log("Игра закрылась!");
        Application.Quit();
    }

    public void OpenSettings()
    {
        Instantiate(_settingWindow.gameObject, transform.parent);
    }

    public void SaveSettings()
    {
        
    }

    public void DzenMode()
    {
        
    }
}
