using Leopotam.Ecs;
using UnityEngine;

namespace Match3 
{ 
    internal class CheckWinLevelTypeSystem : IEcsSystem
    {
        private EcsFilter<WinLevelTypeEvent, DestroyEvent, BlockType, Points> _filter;
        private GameState _gameState;                

        public void Run()
        {           
            if (!_filter.IsEmpty())
            {
                foreach (int index in _filter)
                {
                    ref var destroyEvent = ref _filter.Get2(index);
                    ref var blockType = ref _filter.Get3(index).value;
                    ref var points = ref _filter.Get4(index).value;                    
                }

                if (_gameState._levelType == LevelType.OnlyBlue)
                {
                    //    лопаются только синие гемы                    
                    _gameState.PointsScored++;
                }
                if (_gameState._levelType == LevelType.OnlyRed)
                {
                    //    лопаются только красные гемы
                    _gameState.PointsScored++;
                }
                if (_gameState._levelType == LevelType.OnlyPurple)
                {
                    //    лопаются только красные гемы
                    _gameState.PointsScored++;
                }
                if (_gameState._levelType == LevelType.OnlyGreen)
                {
                    //    лопаются только красные гемы
                    _gameState.PointsScored++;
                }
                if (_gameState._levelType == LevelType.OnlyYellow)
                {
                    //    лопаются только красные гемы
                    _gameState.PointsScored++;
                }
            }
        }
    }
}
