using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class CheckMoveAvailableSystem : IEcsRunSystem
    {
        private EcsFilter<LinkToObject , Position> _filter;
        private GameState _gameState;

        public void Run()
        {
            var count = 0;
            foreach (var index in _filter)
            {
                ref var position = ref _filter.Get2(index).value;
                var board = _gameState.Board;

                count += board.checkBoardMoveAvaliable(position);
            }
            if (count > 0)
            {
                Debug.Log("Доступные ходы ---- " + count);
            }
            //else
            //{
            //board.reShuffle
            //}
        }
    }
}