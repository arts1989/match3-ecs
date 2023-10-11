using Match3;
using UnityEngine;
using UnityEngine.UI;

public class ActivationObj : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private int[] _starsConditional;  //������� - ���������� ���������� �����   
    private int _stars;
    private SaveManager _saveManager;  
   

    private void Awake()
    {
        _saveManager = new SaveManager();
    }

    private void Start()
    {
        _stars = _saveManager.GetData().Stars;
        InitializeButtons();
    }

    private void InitializeButtons() // ������������� ������
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            Button button = _buttons[i];

            int buttonIndex = i;
            ShowUpgradeWindow(buttonIndex);
        }
    }

    private void ShowUpgradeWindow(int buttonIndex) // �������� ���� ���������
    {
        if (_stars >= _starsConditional[buttonIndex])
        {
            _buttons[buttonIndex].interactable = true;
        }
        else
        {
            _buttons[buttonIndex].interactable = false;
        }
    }    
}
