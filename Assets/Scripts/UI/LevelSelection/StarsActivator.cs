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
    private List<LevelData> _levels;
    [SerializeField] private int[] _starsConditional;  //������� - ���������� ���������� �����

    [SerializeField] private Button[] _buttons;

    [SerializeField] private Image[] _emptyIconButton;
    [SerializeField] private Sprite _fillIconButton;

    private void Awake()
    {
        _saveManager = new SaveManager();
    }

    //private void Start()
    //{
    //    _levels = _saveManager.GetData().levels;
    //    //UpgradeIcon();
    //    //StarIconColoring();
    //    InitializeButtons();
    //    //ButtonsIconColoring();
    //}

    //private void Update()
    //{
    //    UpgradeIcon();
    //    // StarIconColoring();
    //}

    //public void UpgradeIcon()  //����� ��������� 
    //{
    //    if (_levels < _upGradeLimitStar)
    //    {
    //        _emptyIconStar[_levels].overrideSprite = _fillIconStar;
    //        StarIconColoring();
    //    }
    //}

    //private void StarIconColoring()  // ������������ ����� �����, ���������� - �� 3 ������ -����������?!!!!
    //{
    //    for (int i = 0; i < _levels; i++)
    //    {
    //        if (_levels % 3 == 0)
    //        {
    //            _emptyIconStar[i].overrideSprite = _fillIconStar;
    //        }
    //    }
    //}

    //private void InitializeButtons() // ������������� ������
    //{
    //    for (int i = 0; i < _buttons.Length; i++)
    //    {
    //        int buttonIndex = i;
    //        ButtonActivationLevel(buttonIndex);
    //    }
    //}

    //private void ButtonActivationLevel(int buttonIndex) // ��������� ������ ������
    //{
    //    if (_levels >= _starsConditional[buttonIndex])
    //    {
    //        _buttons[buttonIndex].enabled = true;
    //        ButtonsIconColoring();
    //    }
    //    else
    //    {
    //        _buttons[buttonIndex].enabled = false; //interactable            
    //    }
    //}

    ////private void ButtonsIconColoring(int buttonIndex)  // ����� ����� ������ ���������� !!!!!!!
    ////{
    ////    for (int i = 0; i < _buttons.Length; i++)
    ////    {
    ////        if (_stars >= _starsConditional[buttonIndex])
    ////        {
    ////            _emptyIconButton[i].overrideSprite = _fillIconButton;
    ////        }            
    ////    }
    ////}

    ////private void ButtonsIconColoring()  // ����� ����� ������ ���������� !!!!!!!
    ////{
    ////    for (int i = 0; i < _buttons.Length; i++)
    ////    {
    ////        _emptyIconButton[i].overrideSprite = _fillIconButton;            
    ////    }
    ////}

    //private void ButtonsIconColoring()  // ����� ����� ������ ���������� !!!!!!!
    //{
    //    for (int i = 0; i < _buttons.Length; i++)
    //    {
    //        _emptyIconButton[i].overrideSprite = _fillIconButton;

    //        if (_levels! % 3 == 0)
    //        {
    //            return;
    //        }
    //    }
    //}
}

