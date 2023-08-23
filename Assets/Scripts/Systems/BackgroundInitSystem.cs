using Leopotam.Ecs;
using UnityEngine;


namespace Match3
{
    internal class BackgroundInitSystem : IEcsInitSystem
    {
        private SceneData _sceneData;
        private GameState _gameState;
        private Configuration _configuration;
        private SaveManager _saveManager;
        private UI _ui;


        public void Init()
        {
            var level = _saveManager.GetData().Level;
            var levelConfig = _configuration.levels[level];
            _sceneData.UI.backgroundSprite.sprite = levelConfig.background;
           // var backgroundObject = Object.Instantiate(_sceneData.UI);

        }
    }
}