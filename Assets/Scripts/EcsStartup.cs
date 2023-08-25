using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    sealed partial class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;

        //[SerializeField] EcsUiEmitter _uiEmitter;

        public Configuration configuration;
        public SceneData sceneData;

        void Start()
        {
            // void can be switched to IEnumerator for support coroutines.

            //load managers 
            var saveManager = new SaveManager();
            var gameState = new GameState();

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                // register your sys tems here, for example:
                .Add(new GameStateInitSystem())
                .Add(new TilemapInitSystem()) // подложка доски
                .Add(new BoardInitSystem()) // спавним ентити, спавним префабы (связанные с энтити)
                .Add(new BoosterInitSystem())
                .Add(new CameraInitSystem()) // устанавливаем камеру над полем
                .Add(new BackgroundInitSystem())
                .Add(new AudioInitSystem())
                .Add(new HandleBoosterSystem())
                .Add(new DetectSwipeSystem())  //пользователь передвигает
                .Add(new CheckMoveSystem()) //проверка можно ли передвинуть
                .Add(new MoveSystem()) // меняет местами
                .Add(new CheckMatchSystem())
                .Add(new ClearUnderlaySystem())
                .Add(new CheckNearbyObstaclesSystem())
                .Add(new UpdatePointsSystem())
                .Add(new DestroySystem())
                .Add(new WaterfallSystem())
                .Add(new SpawnSystem())
                .Add(new CheckWinSystem()) // проверка что есть ентити с WinEvent 
                .Add(new CheckLoseSystem()) // проверка что есть ентити с LoseEvent 

                // register one-frame components (order is important), for example:
                .OneFrame<CheckMoveEvent>()
                .OneFrame<MoveEvent>()
                .OneFrame<CheckMatchEvent>()
                .OneFrame<HandleBoosterEvent>()
                .OneFrame<DestroyEvent>()
                .OneFrame<LoseEvent>()
                .OneFrame<WinEvent>()

                // inject service instances here (order doesn't important), for example:
                .Inject(configuration)
                .Inject(saveManager)
                .Inject(gameState)
                .Inject(sceneData)
                //.InjectUi(_uiEmitter)
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}