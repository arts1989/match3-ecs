using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    internal partial class CheckMoveAvailableSystem : IEcsRunSystem
    {
        private EcsFilter<LinkToObject, Position, BlockType, Points> _filter;
        private GameState _gameState;
        private EcsWorld _world;


        public void Run()
        {
            int count = 0;
            var board = _gameState.Board;

            foreach (var index in _filter)
            {
                // Берём позицию и направления
                var position = _filter.Get2(index);

                //var directions = new List<Vector2Int>() {
                //Vector2Int.up, Vector2Int.down,
                //Vector2Int.right, Vector2Int.left};

                //foreach (var direction in directions) { 
                //    if (board.checkMoveAvaliable(position, direction))
                //    {
                //        count++;
                //     }
                //}
            }

        }
    }
}