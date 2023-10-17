using Leopotam.Ecs;

namespace Match3
{

    internal class UpdateWinPointsSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter<DestroyEvent, WinPoints> _filter;
        private GameState _gameState;
        private SceneData _sceneData;

        public void Init()
        {
             _sceneData.UI.scoreWidget.SetPointsWinText(_gameState.WinPoints);
        }

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                foreach (int index in _filter)
                {
                    ref var points = ref _filter.Get2(index).value;
                    _gameState.WinPoints += points;
                }                

                _sceneData.UI.scoreWidget.SetPointsWinText(_gameState.WinPoints);
                if (_gameState.WinPoints > _gameState.PointsToWin)
                {
                    _filter.GetEntity(0).Get<WinEvent>();
                }
                
                if (_gameState.MovesAvaliable == 0 && _gameState.WinPoints < _gameState.PointsToWin) // fix иначе может быть и вин и луз окна
                {
                    _filter.GetEntity(0).Get<LoseEvent>();
                }
            }
        }
    }
}
