using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    //private _configuration =

    internal class CameraInitSystem : IEcsInitSystem
    {
        private SceneData _sceneData;
        private GameState _gameState;
         
        public void Init() 
        {
            // установка камеры над полем
            var camera = _sceneData.Camera;
            camera.orthographic = true;
            camera.orthographicSize = _gameState.Rows / 2f + .75f;
            // 3 3 - 1 1
            // 4 4 - 1.5 1.5
            _sceneData.CameraTransform.position = new Vector3(
               (_gameState.Rows - 1f) / 2f,
               (_gameState.Columns - 1f) / 2f
            );
        }
    }
}