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
                var sequence = DOTween.Sequence();
                var waterfallLines = new List<int>();      

                foreach (int index in _filter)
                {
                    ref var position = ref _filter.Get2(index).value;

                    if(!waterfallLines.Contains(position.x))
                    {
                        waterfallLines.Add(position.x);
                    }
                }

                var entityInColum = new Dictionary<int, List<EcsEntity>>(); 
                
                foreach(int x in waterfallLines) 
                {
                    var startPos = new Vector2Int(x, 0);
                    var entities = new List<EcsEntity>();

                    while (board.TryGetValue(startPos, out var entity))
                    {
                        entities.Add(entity);
                        startPos += Vector2Int.up;
                    }

                    entityInColum.Add(x, entities);
                }

                foreach (var item in entityInColum)
                {
                    var colums = 0;

                    foreach (var entity in item.Value)
                    {
                        if (!entity.Has<Waterfall>())
                        {
                            var row = item.Value;
                            var coord = new Vector2Int(item.Key, colums);

                            ref var position = ref entity.Get<Position>().value;
                            position = coord;

                            ref var obj = ref entity.Get<LinkToObject>().value;
                            //obj.transform.position = new Vector3(coord.x, coord.y);

                            sequence.Insert(0, obj.transform.DOMove(new Vector3(coord.x, coord.y), .5f));

                            board[coord] = entity;
                            colums++;
                        }
                    }
                }

                foreach (var item in entityInColum)
                {
                    var colums = _gameState.Columns - 1;

                    foreach (var entity in item.Value) 
                    {
                        if (entity.Has<Waterfall>())
                        {
                            var row = item.Value;
                            var coord = new Vector2Int(item.Key, colums);

                            ref var position = ref entity.Get<Position>().value;
                            position = coord;

                            board[coord] = entity;
                            colums--;

                            entity.Del<Waterfall>();
                            entity.Get<Spawn>();
                        }
                    }
                }

                _gameState.enableSpawn = false;
                sequence.Play().OnComplete(() => { _gameState.enableSpawn = true; });
            }
        }
    }
}