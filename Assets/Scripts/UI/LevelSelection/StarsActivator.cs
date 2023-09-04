using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsActivator : MonoBehaviour
{
    [SerializeField] private Image[] _emptyIconStar;
    [SerializeField] private Sprite _fillIconStar;
    [SerializeField] private int _upGradeLimitStar;

    private SaveManager _saveManager;
    private int _stars;
    [SerializeField] private int[] _starsConditional;  //������� - ���������� ���������� �����

    [SerializeField] private Button[] _buttons;

    [SerializeField] private Image[] _emptyIconButton;
    [SerializeField] private Sprite _fillIconButton;    

    private void Awake()
    {
        _saveManager = new SaveManager();
    }

    private void Start()
    {
        _stars = _saveManager.GetData().Stars;
        StarIconColoring();
        InitializeButtons();
        ButtonsIconColoring();
    }

    public void UpgradeIcon()  //����� ���������
    {
        if (_stars < _upGradeLimitStar)
        {
            _emptyIconStar[_stars - 1].overrideSprite = _fillIconStar;
        }
    }

    private void StarIconColoring()  // ������������ ����� �����
    {
        for (int i = 0; i < _stars; i++)
        {
            _emptyIconStar[i].overrideSprite = _fillIconStar;
        }
    }

    private void InitializeButtons() // ������������� ������
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            Button button = _buttons[i];
            int buttonIndex = i;
            ButtonActivationLevel(buttonIndex);
        }
    }

    private void ButtonActivationLevel(int buttonIndex) // ��������� ������ ������
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

    private void ButtonsIconColoring()  // ����� ����� ������
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _emptyIconButton[i].overrideSprite = _fillIconButton;
        }
    }
}
