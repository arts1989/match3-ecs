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
            var gameState = _gameState;

            gameState.currentLevel = level;
            gameState.MovesAvaliable = _configuration.levels[level].MovesAvailable;
            gameState.Rows = levelConfig.Rows;
            gameState.Columns = levelConfig.Columns;
            gameState.ObstacleCount = levelConfig.ObstacleCount;
            gameState.UnderlayCount = levelConfig.UnderlayCount;

            gameState.PointsToWin = _configuration.levels[level].PointsToWin;
            gameState.PointsScored = 0;

            gameState.waterfallSpawnEnable = _configuration.levels[level].waterfallSpawnEnable;
            gameState.background = levelConfig.background;
            gameState.backgroundAudioClip = levelConfig.backgroundSound;

            gameState.swipeSound = _configuration.swipeSound;
            gameState.destroySound = _configuration.destroySound;
            gameState.spawnSound = _configuration.spawnSound;
            gameState.denySound = _configuration.denySound;

            gameState.blocksProperties = levelConfig.blocksProperties;
        }
    }
} 