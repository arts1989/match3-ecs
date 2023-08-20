using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;


namespace Match3
{
    internal class WaterfallSystem : IEcsRunSystem
    {
        private EcsFilter<WaterfallEvent, Position> _filter;
        private GameState _gameState;
        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var board = _gameState.Board;

                foreach (int index in _filter)
                {
                    ref var position = ref _filter.Get2(index).value;

                    var checkPos = position + Vector2Int.up;
                    var currentEntity = _filter.GetEntity(index); 
                    
                    while (board.TryGetValue(checkPos, out var entity))
                    {
                        if(!entity.Has<WaterfallEvent>()) { 

                            var checkPosition = entity.Get<Position>().value;
                            var currentPosition = currentEntity.Get<Position>().value;

                            entity.Get<Position>().value = currentPosition;
                            currentEntity.Get<Position>().value = checkPosition;

                            var obj = entity.Get<LinkToObject>().value;
                            var objPos = new Vector3(currentPosition.x, currentPosition.y);

                            obj.transform.DOMove(objPos, .5f);

                            board[checkPosition] = currentEntity; //позиция сверху меняем на текущую 
                            board[currentPosition] = entity;
                        }

                        checkPos += Vector2Int.up;
                    }

                    currentEntity.Del<WaterfallEvent>();
                    currentEntity.Get<SpawnEvent>();
                }
            }
        }
    }
}