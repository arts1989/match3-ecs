using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
                var board = _gameState.Board;

                ref var direction = ref _filter.Get1(0).direction;
                ref var position  = ref _filter.Get2(0).value;
                
                if (board.checkMoveAvaliable(ref position, ref direction))
                {
                    board[position].Get<MoveEvent>();
                    board[position + direction].Get<MoveEvent>();
                } 
                else
                {
                    var entity = board[position];
                    var obj = entity.Get<LinkToObject>().value;
                    entity.Get<DenyEvent>();

                    var pos     = new Vector3(position.x, position.y);
                    var nearPos = new Vector3(
                        position.x + (float)direction.x / 4,
                        position.y + (float)direction.y / 4
                    );

                    _gameState.freezeBoard = true;
                    obj.transform.DOMove(nearPos, .25f).OnComplete(
                        () => obj.transform.DOMove(pos, .25f).OnComplete(
                            () => _gameState.freezeBoard = false
                        )
                    );
                }
            }
        } 
    }  
}