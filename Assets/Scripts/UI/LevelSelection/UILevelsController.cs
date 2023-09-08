using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelsController : MonoBehaviour
{
    [SerializeField] private List<UIStars> _listUIStars;
    private SaveManager _saveManager;
    private List<LevelData> _levels;

    private void Start()
    {
        _saveManager = new SaveManager();
        _levels = _saveManager.GetData().levels;
        
        if (_levels != null)
        {
            for (int i = 0; i < _listUIStars.Count; i++)
            {
                if (_levels.Count - 1 < i)
                    return;
                if (_levels[i] != null)
                {
                    _listUIStars[i].Init(_levels[i].Stars);
                }
            }
        }
    }
}
