using Match3;
using UnityEngine;
using UnityEngine.UI;

public class ActivationObj : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private int _star = 3;  //заглушка для проверки  
    private SaveManager _saveManager;

    void Awake()
    {
        _saveManager = new SaveManager();
    }
    void Start()
    {
        var Stars = _saveManager.GetData().Stars;
        if (_star >= Stars)
        {
            _button.interactable = true;                        
        }
        else
        {
            _button.interactable = false;           
        }        
    }    
}
