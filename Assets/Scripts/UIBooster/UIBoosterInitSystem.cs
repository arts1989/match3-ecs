using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;
using UnityEngine.UI;

internal sealed class UIBoosterInitSystem : IEcsInitSystem
{
    EcsUiEmitter _ui;
    
    public void Init()
    {
    }

    private void Start()
    {
        Debug.Log("test1");
    }
}
