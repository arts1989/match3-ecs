using Leopotam.Ecs;
using Match3;
using UnityEngine;

namespace Systems
{
    internal class CheckMoveSystem : IEcsRunSystem
    {
        private EcsFilter<CheckMoveEvent, LinkToObject, Position, BlockType> _filter;
        private GameState _gameState;

        private CheckingNeighboringGems _checkingNeighboringGems;
        
        public void Run()
        {
            //system to check if the heme can be moved in the direction
            //vector - to the next row in a combination or leave it in place
            
            if (!_filter.IsEmpty())
            {
                ref var direction = ref _filter.Get1(0).direction;
                ref var currentPosition = ref _filter.Get3(0).value;
                ref var currentBlockType = ref _filter.Get4(0).value;
                
                var board = _gameState.Board;
                
                if()
                
            }
        }
    }  
}
