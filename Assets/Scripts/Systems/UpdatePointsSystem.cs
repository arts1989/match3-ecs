using Leopotam.Ecs;

namespace Match3
{
    internal class UpdatePointsSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter<SpawnEvent, Points> _filter;
        private GameState _gameState;
        private SceneData _sceneData;

        public void Init()
        {
            _sceneData.UI.scoreWidget.SetMovesLeftText(_gameState.MovesAvaliable);
            _sceneData.UI.scoreWidget.SetPointsScoredText(_gameState.PointsScored);
        }

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                foreach (int index in _filter)
                {
                    ref var points = ref _filter.Get2(index).value;
                    _gameState.PointsScored += points;
                }

                _gameState.MovesAvaliable--;

                _sceneData.UI.scoreWidget.SetPointsScoredText(_gameState.PointsScored);
                if (_gameState.PointsScored > _gameState.PointsToWin)
                {
                    _filter.GetEntity(0).Get<WinEvent>();
                }

                _sceneData.UI.scoreWidget.SetMovesLeftText(_gameState.MovesAvaliable);
                if (_gameState.MovesAvaliable == 0 && _gameState.PointsScored < _gameState.PointsToWin) // fix иначе может быть и вин и луз окна
                {
                    _filter.GetEntity(0).Get<LoseEvent>();
                }
            }
        }
    }
}