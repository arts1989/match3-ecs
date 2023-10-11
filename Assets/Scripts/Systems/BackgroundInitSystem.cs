using Leopotam.Ecs;


namespace Match3
{
    internal class BackgroundInitSystem : IEcsInitSystem
    {
        private GameState _gameState;
        private SceneData _sceneData;

        public void Init()
        {
            _sceneData.background.sprite = _gameState.background;
        }
    }
}