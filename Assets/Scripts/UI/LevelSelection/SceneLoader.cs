using Match3;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public Configuration Configuration;
    private SaveManager _saveManager;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _saveManager = new SaveManager();
    }

    public void StartLevel(int numberLevel)
    {
        var saveData = _saveManager.GetData();
        if(saveData.levels == null)
        {
            saveData.levels = new System.Collections.Generic.List<LevelData>();
        }
        if (saveData.levels.Count > numberLevel)
        {
            saveData.levels.RemoveAt(numberLevel);
        }
        saveData.levels.Insert(numberLevel, new LevelData() { Stars = 0 });
        _saveManager.SaveData(saveData);
        SceneManager.LoadScene("Game");
    }
}
