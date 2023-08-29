using Leopotam.Ecs;
using Match3;
using System.Collections;
using UnityEngine;

namespace Match3
{
    public class AudioRunSystem : IEcsRunSystem
    {
        private EcsFilter <DestroyEvent> _filter;
        private SceneData _sceneData;
        private GameState _gameState;
        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var destroySound = _gameState.destroyBlockSound;
                _sceneData.destroyBlockSound.Play();
                _sceneData.destroyBlockSound.volume = 1f;
            }
        }
    }
}