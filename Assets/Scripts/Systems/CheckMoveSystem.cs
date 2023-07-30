﻿using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class CheckMoveSystem : IEcsRunSystem
    {
        private EcsFilter<CheckMoveEvent, Position> _filter;
        private GameState _gameState;

        public void Run()
        {
            //система для проверки можно ли двигать гем в направлении
            //вектора - в соседний ряд в комбинацию или оставить на месте.
            if (!_filter.IsEmpty())
            {
                ref var direction       = ref _filter.Get1(0).direction;
                ref var currentPosition = ref _filter.Get2(0).value;

                var board = _gameState.Board;

                if (board.checkMoveAvaliable(currentPosition, direction))
                {
                    board[currentPosition].Get<MoveEvent>();
                    board[currentPosition + direction].Get<MoveEvent>();
                } else
                {
                    Debug.Log("Движение запрещено");
                }
            }
        }
    }  
}