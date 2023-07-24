using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class CheckMoveSystem : IEcsRunSystem
    {
        private EcsFilter<CheckMoveEvent, LinkToObject, Position> _filter;
        private GameState _gameState;

        public void Run()
        {
            //система для проверки можно ли двигать гем в направлении
            //вектора - в соседний ряд в комбинацию или оставить на месте.
            if (!_filter.IsEmpty())
            {
                ref var direction = ref _filter.Get1(0).direction;
                ref var currentPosition = ref _filter.Get3(0).value;

                var board = _gameState.Board;
                var neighboringPosition = currentPosition + direction;

                var currentEntity = board[currentPosition];
                var neighboringEntity = board[neighboringPosition];

                currentEntity.Get<MoveEvent>();
                neighboringEntity.Get<MoveEvent>();
            }
        }
    }  
}