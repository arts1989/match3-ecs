using Leopotam.Ecs;

namespace Match3
{
    internal class GameStateInitSystem : IEcsInitSystem
    {
        private Configuration _configuration;
        private GameState _gameState;
        private SaveManager _saveManager;

        public void Init()
        {
            var level = _saveManager.GetData().Level;
            var levelConfig = _configuration.levels[level];

            _gameState.currentLevel = level;
            _gameState.MovesAvaliable = _configuration.levels[level].MovesAvailable;
            _gameState.Rows = levelConfig.Rows;
            _gameState.Columns = levelConfig.Columns;
            _gameState.ObstacleCount = levelConfig.ObstacleCount;
            _gameState.UnderlayCount = levelConfig.UnderlayCount;

            _gameState.PointsToWin = _configuration.levels[level].PointsToWin;
            _gameState.PointsScored = 0;

            _gameState.waterfallSpawnEnable = _configuration.levels[level].waterfallSpawnEnable; 
            _gameState.background = levelConfig.background;
            _gameState.backgroundAudioClip = levelConfig.backgroundSound;

        }
    }
}