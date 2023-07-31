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
            //������� �������� ����� ������ ����������
            foreach (var index in _filter)
            {
                ref var position = ref _filter.Get2(index).value;

                //Debug.Log("������� ������: "  + position);

                var board = _gameState.Board;

                foreach (var coords in board.getMatchCoords(position, _configuration.minChainLenght))
                {
                    board[coords].Get<DestroyEvent>();
                }

                break; //�������� �������� ����� ���� ������ ����� �����, ������
            }
        }
    }
}