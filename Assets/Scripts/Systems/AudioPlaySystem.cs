using Leopotam.Ecs;

namespace Match3
{
    internal class AudioPlaySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<MoveEvent> _moveEvent;
        private EcsFilter<DestroyEvent> _destroyEvent;
        private EcsFilter<SpawnEvent> _spawnEvent;
        private EcsFilter<MoveBlockedEvent> _moveBlockedEvent;

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
                _sceneData.swipeSound.Play();
                _sceneData.swipeSound.volume = 0.5f;
            }

            if (!_destroyEvent.IsEmpty())
            {
                _sceneData.destroyBlockSound.Play();
                _sceneData.destroyBlockSound.volume = 1f;
            }

            if (!_spawnEvent.IsEmpty())
            {
                _sceneData.spawnEventSound.Play();
                _sceneData.spawnEventSound.volume = 1f;
            }
            if(!_moveBlockedEvent.IsEmpty())
            {
                _sceneData.denyMoveSound.Play();
                _sceneData.denyMoveSound.volume = 1f;
            }
        }
    }
}