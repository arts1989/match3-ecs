using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLvl : MonoBehaviour
{
    [SerializeField] private int _level; //для указания в инспекторе какой уровень подгружать

    public void StartLevelButton()
    {
        SceneLoader.Instance.StartLevel(_level); 
    }
}
