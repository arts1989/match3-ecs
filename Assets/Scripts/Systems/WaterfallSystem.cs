using Cysharp.Threading.Tasks;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;


namespace Match3
{
    internal class WaterfallSystem : IEcsRunSystem
    {
        private EcsFilter<Waterfall, Position> _filter;
        private GameState _gameState;
        public  void Run()
        {
            if (!_filter.IsEmpty())
            {
                var board = _gameState.Board;
                //var sequence = DOTween.Sequence();
                var entityChangedPos = new List<EcsEntity>();

                foreach (int index in _filter)
                {
                    ref var position = ref _filter.Get2(index).value;

                    var checkPos = position + Vector2Int.up;
                    var currentEntity = _filter.GetEntity(index); 
                    
                    while (board.TryGetValue(checkPos, out var entity))
                    {
                        if(!entity.Has<Waterfall>()) { 

                            var checkPosition = entity.Get<Position>().value;
                            var currentPosition = currentEntity.Get<Position>().value;

                            entity.Get<Position>().value = currentPosition;
                            currentEntity.Get<Position>().value = checkPosition;

                            entityChangedPos.Add(entity);

                            board[checkPosition] = currentEntity; //позиция сверху меняем на текущую 
                            board[currentPosition] = entity;
                        }

                        checkPos += Vector2Int.up;
                    }

                    currentEntity.Del<Waterfall>();
                    currentEntity.Get<Spawn>();
                }

                foreach(var entity in entityChangedPos) 
                { 
                    ref var pos = ref entity.Get<Position>().value;
                    ref var obj = ref entity.Get<LinkToObject>().value;
                    obj.transform.position = new Vector3(pos.x, pos.y);
                    //obj.transform.DOMove(new Vector3(pos.x, pos.y), .5f);
                    //sequence.Insert(0, obj.transform.DOMove(new Vector3(pos.x, pos.y), .5f));
                }

                //_gameState.enableSpawn = false;
                //sequence.Play().OnComplete(() => { _gameState.enableSpawn = true; });
            }
        }
    }
}