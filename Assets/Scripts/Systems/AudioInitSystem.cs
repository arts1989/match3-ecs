using Leopotam.Ecs;

namespace Match3
{
    internal class AudioInitSystem : IEcsInitSystem
    {
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
    }
}