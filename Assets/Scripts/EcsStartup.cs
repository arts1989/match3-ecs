using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    sealed partial class EcsStartup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;

        public GameConfig gameConfig;
        public SceneData sceneData;
        
        void Start () {
            // void can be switched to IEnumerator for support coroutines.

            //load managers 
            SaveManager saveManager = new SaveManager();
            var levelConfig = gameConfig.levels[saveManager.GetData().Level];
            var board = new Board(levelConfig.Columns, levelConfig.Rows);

            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                // register your sys tems here, for example:
                .Add (new BoardInitSystem()) // спавним ентити, спавним префабы (связанные с энтити)
                .Add (new CameraInitSystem()) // устанавливаем камеру над полем
                .Add (new DetectSwipeSystem())  //пользователь передвигает
                .Add (new CheckMoveSystem ()) //проверка можно ли передвинуть
                .Add (new MoveSystem()) // меняет местами
                .Add (new DestroySystem()) // унитожает связанный с энтити геймобжект
                .Add (new SpawnSystem ()) // спавнит новый и связывает с энтитей

                // register one-frame components (order is important), for example:
                .OneFrame<CheckMoveEvent> ()
                .OneFrame<MoveEvent> ()
                .OneFrame<DestroyEvent> ()
                .OneFrame<SpawnEvent> ()

                // inject service instances here (order doesn't important), for example:
                .Inject (gameConfig)
                .Inject (saveManager)
                .Inject (board)
                .Inject (sceneData)
                .Init ();
        }

        void Update () { 
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}