using Leopotam.Ecs;


namespace Match3
{
    internal class BackgroundInitSystem : IEcsInitSystem
    {
        private GameState _gameState;

        public void Init()
        {
            _sceneData.background.sprite = _gameState.background;
        }
    }
}