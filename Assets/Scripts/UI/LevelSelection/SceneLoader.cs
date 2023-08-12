using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public Configuration _configuration;
    private SaveManager _saveManager;
    
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _saveManager = new SaveManager();
    }

    public void StartLevel(int numberLevel)
    {
        _saveManager.SaveData(new SaveData() { Level = numberLevel });
        SceneManager.LoadScene("Game");
    }
}
