using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class CheckMatchSystem : IEcsRunSystem
    {
        private EcsFilter<CheckMatchEvent, Position> _filter;
        private GameState _gameState;
        private Configuration _configuration;

        public void Run()
        {
            //система помечает какие ентити уничтожить
            foreach (var index in _filter)
            {
                ref var position = ref _filter.Get2(index).value;

                //Debug.Log("позиция энтети: "  + position);

                var board = _gameState.Board;

                foreach (var coords in board.getMatchCoords(position, _configuration.minChainLenght))
                {
                    board[coords].Get<DestroyEvent>();
                }

                break; //отладить проверку линий куда пришел кубик сосед, глючит
            }
        }
    }
}