using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using Leopotam.Ecs.UnityIntegration;
using UnityEngine;

namespace Match3
{
    internal sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private EcsUiEmitter _uiEmitter;

        public Configuration configuration;
        public SceneData sceneData;
        private EcsSystems _systems;
        private EcsWorld _world;

        private void Start()
        {
            // void can be switched to IEnumerator for support coroutines.

            //load managers 
            var saveManager = new SaveManager();
            var gameState = new GameState();

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            EcsWorldObserver.Create(_world);
            EcsSystemsObserver.Create(_systems);
#endif
            _systems
                // register your sys tems here, for example:
                .Add(new GameStateInitSystem())
                .Add(new TilemapInitSystem())
                .Add(new BoardInitSystem())
                .Add(new UIBoosterInitSystem())
                .Add(new CameraInitSystem())
                .Add(new BackgroundInitSystem())
                .Add(new HandleUIBoosterSystem())
                .Add(new DetectSwipeSystem())
                .Add(new CheckMoveSystem())
                .Add(new BoosterMergeSystem())
                .Add(new MoveSystem())
                .Add(new CheckMatchSystem())
                .Add(new ClearUnderlaySystem())
                .Add(new CheckNearbyObstaclesSystem())
                .Add(new UpdatePointsSystem())
                .Add(new DestroySystem())
                .Add(new WaterfallSystem())
                .Add(new SpawnSystem())
                .Add(new AudioPlaySystem())
                .Add(new CheckWinSystem())
                .Add(new CheckLoseSystem())
                .Add(new CheckWinConditionSystem())

                // register one-frame components (order is important), for example:
                .OneFrame<CheckMoveEvent>()
                .OneFrame<MoveEvent>()
                .OneFrame<CheckMatchEvent>()
                .OneFrame<DestroyEvent>()
                .OneFrame<LoseEvent>()
                .OneFrame<WinEvent>()
                .OneFrame<SpawnEvent>()
                .OneFrame<DenyEvent>()

                // inject service instances here (order doesn't important), for example:
                .Inject(configuration)
                .Inject(saveManager)
                .Inject(gameState)
                .Inject(sceneData)
                .InjectUi(_uiEmitter)
                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
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