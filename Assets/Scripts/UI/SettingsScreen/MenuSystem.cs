using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[Serializable]
public class MenuEntry
{
    public string EntryName;
    public UnityEvent Callback;
}

[RequireComponent(typeof(UIDocument))]
public class MenuSystem : MonoBehaviour
{
    [SerializeField] private List<MenuEntry> _menuEntries;
    [SerializeField] private float _transitionDuration;
    [SerializeField] private EasingMode _easing;
    [SerializeField] private float _buttonDelay;
    [SerializeField] private string _animatedClass;
    [SerializeField] private VisualTreeAsset _buttonTemplate;
    private VisualElement _container;
    private List<TimeValue> _durationValues;
    private StyleList<EasingFunction> _easingValues;
    private WaitForSeconds _pause;
    
   /* Для вырианта меню Toolkit
    
    private void Start()
    {
        _durationValues = new List<TimeValue> { new(_transitionDuration, TimeUnit.Second) };
        _easingValues = new StyleList<EasingFunction>(new List<EasingFunction> { new(_easing) });
        StartCoroutine(CreateMenu());
    }

    private IEnumerator CreateMenu()
    {
        yield return new WaitForSeconds(1);
        _container = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("container");
        foreach (var entry in _menuEntries)
        {
            VisualElement newElement = _buttonTemplate.CloneTree();
            var button = newElement.Q<Button>("menu-button");
            button.Q<Label>("label").text = entry.EntryName;
            button.clicked += delegate { OnClick(entry); };
            _container.Add(newElement);
            newElement.style.transitionDuration = _durationValues;
            newElement.style.transitionTimingFunction = _easingValues;
            newElement.AddToClassList(_animatedClass);
            yield return null;
            newElement.RemoveFromClassList(_animatedClass);
            yield return _pause;
        }
    }

    private void OnClick(MenuEntry entry)
    {
        entry.Callback.Invoke();
    } 
    */
}