using Match3;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsActivator : MonoBehaviour
{
    public static ButtonsActivator Instance;
    private SaveManager _saveManager;
    private List<LevelData> _levels;

    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image[] _emptyIconButton;
    [SerializeField] private Sprite _fillIconButton;
    [SerializeField] private int[] _starsConditional;  //условие - количество полученных звезд 

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _saveManager = new SaveManager();
    }

    private void Start()
    {
        _levels = _saveManager.GetData().levels;
        InitializeButtons();
    }

    private void InitializeButtons() // Инициализация кнопок
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            int buttonIndex = i;
            ButtonActivationLevel(buttonIndex);
        }
    }

    private void ButtonActivationLevel(int buttonIndex) // активация кнопки уровня и смена цвета
    {
        if (_levels != null)
        {
            if (_levels.Count >= _starsConditional[buttonIndex])
            {
                _buttons[buttonIndex].enabled = true;
                _emptyIconButton[buttonIndex].overrideSprite = _fillIconButton;
            }
            else
            {
                _buttons[buttonIndex].enabled = false;
            }
        }
    }
}
