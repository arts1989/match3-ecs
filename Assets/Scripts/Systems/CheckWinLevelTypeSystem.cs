using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class CheckWinLevelTypeSystem : IEcsRunSystem
    {
        private EcsFilter<DestroyEvent, BlockType, WinPoints> _filter;
        private GameState _gameState;

        public void Run()
        {
            if (_filter.IsEmpty()) return;
            foreach (var index in _filter)
            {
                ref var blockType = ref _filter.Get2(index).value;
                ref var winPoints = ref _filter.Get3(index).value;

                switch (blockType)
                {
                    case BlockTypes.Blue when _gameState.LevelType == LevelTypes.OnlyBlue:
                        _gameState.WinPoints += winPoints;
                        break;
                    case BlockTypes.Red when _gameState.LevelType == LevelTypes.OnlyRed:
                        Debug.Log("OnlyRed");
                        _gameState.WinPoints += winPoints;
                        break;
                    case BlockTypes.Purple when _gameState.LevelType == LevelTypes.OnlyPurple:
                    case BlockTypes.Green when _gameState.LevelType == LevelTypes.OnlyGreen:
                    case BlockTypes.Yellow when _gameState.LevelType == LevelTypes.OnlyYellow:
                        _gameState.WinPoints += winPoints;
                        break;
                }


                if (_gameState.LevelType == LevelTypes.Substrate) _gameState.WinPoints += winPoints;
                if (_gameState.LevelType == LevelTypes.CombinationT &&
                    _gameState.LevelType == LevelTypes.CombinationSquare) _gameState.WinPoints += winPoints;
            }
        }
    }
}