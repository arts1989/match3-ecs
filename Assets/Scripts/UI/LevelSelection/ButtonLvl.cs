using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLvl : MonoBehaviour
{
    [SerializeField] private int _level; //��� �������� � ���������� ����� ������� ����������

    public void StartLevelButton()
    {
        SceneLoader.Instance.StartLevel(_level); 
    }
}
