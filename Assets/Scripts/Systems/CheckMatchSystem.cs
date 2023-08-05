using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class CheckMatchSystem : IEcsRunSystem
    {
        private EcsFilter<CheckMatchEvent, Position> _filter;
        private GameState _gameState;

        public void Run()
        {
            //система помечает какие ентити уничтожить
            foreach (var index in _filter)
            {
                ref var oldPosition = ref _filter.Get1(index).oldPosition;
                ref var position    = ref _filter.Get2(index).value;

                var board = _gameState.Board;

                foreach (var coords in board.getMatchCoords(position, oldPosition))
                {
                    board[coords].Get<DestroyEvent>();
                }
            }
        }
    }
}