using Leopotam.Ecs;
using UnityEngine;


namespace Match3
{
    internal class BackgroundInitSystem : IEcsInitSystem
    {
        private SceneData _sceneData;
        private GameState _gameState;
        private Configuration _configuration;

        public void Init()
        {
            foreach (var background in _configuration.levels)
            {
               
            }    
        }
    }
}