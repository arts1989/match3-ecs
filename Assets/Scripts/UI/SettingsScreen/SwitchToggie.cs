using UnityEngine;
using UnityEngine.UI;

public class SwitchToggie : MonoBehaviour
{
    [SerializeField] private RectTransform uiHandleRectTransform;
    private Toggle _toggle;
    private Vector2 _handlePosition;
    [SerializeField] private GameObject _pictureActive;
    [SerializeField] private GameObject _picturePassive;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _handlePosition = uiHandleRectTransform.anchoredPosition;
        _toggle.onValueChanged.AddListener(OnSwitch);
        if (_toggle.isOn)
            OnSwitch(true);
    }

    private void OnSwitch(bool on)
    {
        if (on)
        {
            uiHandleRectTransform.anchoredPosition = _handlePosition * -1;
            _pictureActive.SetActive(true);
            _picturePassive.SetActive(false);
        }
        else
        {
            uiHandleRectTransform.anchoredPosition = _handlePosition;
            _pictureActive.SetActive(false);
            _picturePassive.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
