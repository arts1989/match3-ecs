using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal partial class CheckWinSystem : IEcsRunSystem
    {
        private EcsFilter<WinEvent> _filter;
        private SceneData _sceneData;

        public void Run()
        {
            foreach(var index in _filter)
            {
               // Debug.Log(_filter.ToString());
                _filter.GetEntity(index).Del<WinEvent>();
                _sceneData.UI.WinScreen.Show(true);
            }
        }
    }
} 