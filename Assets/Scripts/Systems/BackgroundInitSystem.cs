using Leopotam.Ecs;
using UnityEngine;


namespace Match3
{
    internal class BackgroundInitSystem : IEcsInitSystem
    {
        private SceneData _sceneData;
        private Configuration _configuration;
        private SaveManager _saveManager;


        public void Init()
        {
            var level = _saveManager.GetData().Level;
            var levelConfig = _configuration.levels[level];

            _sceneData.background.sprite = levelConfig.background;

        }
    }
}