using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

namespace Match3
{
    internal class AudioInitSystem : IEcsInitSystem
    {
        private SceneData _sceneData;
        private GameState _gameState;
        public void Init()
        {
           
            var backgroundAudioClip =  _gameState.backgroundAudio; 
             backgroundAudioClip = _sceneData.audioView.backgroundAudio.GetComponent<AudioClip>();
        }
    }
}