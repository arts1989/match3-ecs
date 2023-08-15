using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;


namespace Match3
{
    sealed partial class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;

        [SerializeField] EcsUiEmitter _uiEmitter;

        public Configuration configuration;
        public SceneData sceneData;

        void Start()
        {
            // void can be switched to IEnumerator for support coroutines.

            //load managers 
            var saveManager = new SaveManager();
            var gameState = new GameStates();

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                // register your sys tems here, for example:
                .Add(new GameStateInitSystem())
                .Add(new UpdateScoreWidgetSystem())
                .Add(new TilemapInitSystem()) // �������� �����
                .Add(new BoardInitSystem()) // ������� ������, ������� ������� (��������� � ������)
                .Add(new BoosterInitSystem())
                .Add(new CameraInitSystem()) // ������������� ������ ��� �����
                .Add(new HandleBoosterDestroyBlocksSameTypeSystem())
                .Add(new HandleBoosterDestroyBlocksInRowSystem())
                .Add(new DetectSwipeSystem())  //������������ �����������
                .Add(new CheckMoveSystem()) //�������� ����� �� �����������
                .Add(new MoveSystem()) // ������ �������
                .Add(new CheckMatchSystem())
                .Add(new DestroyAndSpawnSystem()) // ��������� ��������� � ������ ����������
                .Add(new CheckWinSystem()) // �������� ��� ���� ������ � WinEvent 
                .Add(new CheckLoseSystem()) // �������� ��� ���� ������ � LoseEvent 


                // register one-frame components (order is important), for example:
                .OneFrame<CheckMoveEvent>()
                .OneFrame<MoveEvent>()
                .OneFrame<CheckMatchEvent>()
                .OneFrame<HandleBoosterEvent>()
                .OneFrame<DestroyAndSpawnEvent>()


                // inject service instances here (order doesn't important), for example:
                .Inject(configuration)
                .Inject(saveManager)
                .Inject(gameState)
                .Inject(sceneData)
                .InjectUi(_uiEmitter)
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