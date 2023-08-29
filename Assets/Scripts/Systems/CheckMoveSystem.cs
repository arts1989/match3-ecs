using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class CheckMoveSystem : IEcsRunSystem
    {
        private EcsFilter<CheckMoveEvent, Position> _filter;
        private GameState _gameState;
        private SceneData _sceneData;

        public void Run()
        {
            //система для проверки можно ли двигать гем в направлении
            //вектора - в соседний ряд в комбинацию или оставить на месте.
            if (!_filter.IsEmpty())
            {
                ref var direction = ref _filter.Get1(0).direction;
                ref var position  = ref _filter.Get2(0).value;

                var board = _gameState.Board;

                if (board.checkMoveAvaliable(ref position, ref direction))
                {
                    board[position].Get<MoveEvent>();
                    board[position + direction].Get<MoveEvent>();
                    var swipeSound = _gameState.swipeSound;
                    _sceneData.swipeSound.Play();
                    _sceneData.swipeSound.volume = 0.5f;
                } else
                {
                    Debug.Log("Движение запрещено");
                }
            }
        }
    }  
}