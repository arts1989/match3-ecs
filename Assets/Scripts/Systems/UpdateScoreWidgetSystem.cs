using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal partial class UpdateScoreWidgetSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter<UpdateScoreEvent> _filter;
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
                _gameState.MovesAvaliable--;
                
                _sceneData.UI.scoreWidget.SetPointsScoredText(_gameState.PointsScored);
                if(_gameState.PointsScored > _gameState.PointsToWin)
                {
                    _filter.GetEntity(0).Get<WinEvent>();
                }

                _sceneData.UI.scoreWidget.SetMovesLeftText(_gameState.MovesAvaliable);
                if (_gameState.MovesAvaliable == 0 && _gameState.PointsScored < _gameState.PointsToWin) // fix иначе может быть и вин и луз окна
                {
                    _filter.GetEntity(0).Get<LoseEvent>();
                }

                _filter.GetEntity(0).Del<UpdateScoreEvent>(); //destroy entity with UpdateScoreEvent component
            }
        }
    }
}



