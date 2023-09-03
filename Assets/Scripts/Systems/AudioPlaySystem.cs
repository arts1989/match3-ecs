using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class AudioPlaySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<MoveEvent> _moveEvent;
        private EcsFilter<DestroyEvent> _destroyEvent;
        private EcsFilter<SpawnEvent> _spawnEvent;

        private SceneData _sceneData;
        private GameState _gameState;

        public void Init()
        {
            var backgroundMusic = _gameState.backgroundAudioClip;
            _sceneData.backgroundMusic.clip = backgroundMusic;
            _sceneData.backgroundMusic.Play();
            _sceneData.backgroundMusic.volume = 0.2f;
            _sceneData.backgroundMusic.loop = true;
        }

        public void Run()
        {
            if (!_moveEvent.IsEmpty())
            {
                var blocksAudio = _sceneData.blocksAudio.GetComponent<AudioSource>();
                blocksAudio.clip = _gameState.swipeSound;

                blocksAudio.volume = 0.5f;
                blocksAudio.Play();
            }

            if (!_destroyEvent.IsEmpty())
            {
                var blocksAudio = _sceneData.blocksAudio.GetComponent<AudioSource>();
                blocksAudio.clip = _gameState.destroySound;

                blocksAudio.volume = 0.5f;
                blocksAudio.Play();
            }

            if (!_spawnEvent.IsEmpty())
            {
                var blocksAudio = _sceneData.blocksAudio.GetComponent<AudioSource>();
                blocksAudio.clip = _gameState.spawnSound;

                blocksAudio.volume = 0.5f;
                blocksAudio.Play();
            }
        }
    }
}