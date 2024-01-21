using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingWindow : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void SaveButton()
    {
        
    }

    public void TutorialButton()
    {
        
    }
    
    public void VibrationButton()
    {
        Handheld.Vibrate();
    }

    public void MessageButton()
    {
        
    }
}
