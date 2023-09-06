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
            //система помечает какие ентити уничтожить и прокидывает тип блока который спавнить на месте старой
            foreach (var index in _filter)
            {
                ref var oldPosition = ref _filter.Get1(index).oldPosition;
                ref var position    = ref _filter.Get2(index).value;

                var board = _gameState.Board;
                var matchCoords = board.getMatchCoords(ref position, ref oldPosition);

                Debug.Log(matchCoords.blockType + " ========== " + matchCoords.coords.Count);

                foreach (var coords in matchCoords.coords)
                {
                    board[coords].Get<SpawnType>().value = (coords == position) ? matchCoords.blockType : BlockTypes.Default;
                    board[coords].Get<DestroyEvent>();
                }
            }
        }
    }
}