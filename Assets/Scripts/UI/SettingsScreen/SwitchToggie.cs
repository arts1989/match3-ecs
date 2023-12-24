using System;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggie : MonoBehaviour
{
    [SerializeField] private RectTransform uiHandleRectTransform;
    private Toggle _toggle;
    private Vector2 _handlePosition;
    void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _handlePosition = uiHandleRectTransform.anchoredPosition;
        _toggle.onValueChanged.AddListener(OnSwitch);
        if(_toggle.isOn)
            OnSwitch(true);
    }

    private void OnSwitch(bool on)
    {
        uiHandleRectTransform.anchoredPosition = on ? _handlePosition * -1 : _handlePosition;
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
