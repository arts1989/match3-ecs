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
            if (!_filter.IsEmpty())
            {
                foreach (int index in _filter)
                {
                    ref var blockType = ref _filter.Get2(index).value;
                    ref var winPoints = ref _filter.Get3(index).value;                    

                    if (blockType == BlockTypes.Blue && _gameState.LevelType == LevelTypes.OnlyBlue)
                    {
                        _gameState.WinPoints += winPoints;
                    }
                    if (blockType == BlockTypes.Red && _gameState.LevelType == LevelTypes.OnlyRed)
                    {
                        Debug.Log("OnlyRed");
                        _gameState.WinPoints += winPoints;
                    }
                    if (blockType == BlockTypes.Purple && _gameState.LevelType == LevelTypes.OnlyPurple)
                    {                        
                        _gameState.WinPoints += winPoints;
                    }
                    if (blockType == BlockTypes.Green && _gameState.LevelType == LevelTypes.OnlyGreen)
                    {                        
                        _gameState.WinPoints += winPoints;
                    }
                    if (blockType == BlockTypes.Yellow && _gameState.LevelType == LevelTypes.OnlyYellow)
                    {                        
                        _gameState.WinPoints += winPoints;
                    }


                    if (_gameState.LevelType == LevelTypes.Substrate)
                    {
                        _gameState.WinPoints += winPoints;
                    }
                    if (_gameState.LevelType == LevelTypes.CombinationT && _gameState.LevelType == LevelTypes.CombinationSquare)
                    {
                        _gameState.WinPoints += winPoints;
                    }
                }
            }
        }
    }
}
