using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class AudioInitSystem : IEcsInitSystem
    {
        private SceneData _sceneData;
        private GameState _gameState;
        public void Init()
        {
            var obj = _gameState.backgroundAudioClip;
           _sceneData.backgroundMusic.clip = obj;
            _sceneData.backgroundMusic.Play();
            _sceneData.backgroundMusic.loop = true;
        }
    }
}