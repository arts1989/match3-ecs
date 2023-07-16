using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    //private _configuration =

    internal class CameraInitSystem : IEcsInitSystem
    {
        private SceneData _sceneData;
        private GameConfig _gameConfig;
        private SaveManager _saveManager;
         
        public void Init() 
        {
            // установка камеры над полем
            var levelConfig = _gameConfig.levels[_saveManager.GetData().Level];

            var camera = _sceneData.Camera;
            camera.orthographic = true;
            camera.orthographicSize = levelConfig.Rows / 2f;
            // 3 3 - 1 1
            // 4 4 - 1.5 1.5
            _sceneData.CameraTransform.position = new Vector3(
               (levelConfig.Rows - 1f) / 2f,
               (levelConfig.Columns - 1f) / 2f
            );
        }
    }
}