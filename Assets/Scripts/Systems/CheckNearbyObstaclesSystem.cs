using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    internal class CheckNearbyObstaclesSystem : IEcsRunSystem
    {
        private EcsFilter<DestroyEvent, Position> _filter;
        private GameState _gameState;
        private Configuration _configuration;
        private EcsWorld _world;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var nearbyObstaclesCoords = new List<Vector2Int>();
                var board = _gameState.Board;

                foreach (int index in _filter)
                {
                    ref var position = ref _filter.Get2(index).value;

                    //запишем соседей-ящики
                    var nearbyObstacles = board.getNearbyObstacles(ref position);
                    if (nearbyObstacles.Count > 0)
                    {
                        foreach (var coord in nearbyObstacles)
                        {
                            if (!nearbyObstaclesCoords.Contains(coord))
                            {
                                nearbyObstaclesCoords.Add(coord);
                            }
                        }
                    }
                }

                if (nearbyObstaclesCoords.Count > 0)
                {
                    foreach (var obstacleCoord in nearbyObstaclesCoords)
                    {
                        var entity = board[obstacleCoord];

                        var obj = entity.Get<LinkToObject>().value;
                        ref var health = ref entity.Get<Health>().value;

                        //h - 4 c - 0
                        //h - 3 c - 1 -- 4 - 3 = 1
                        //h - 2 c - 2 -- 4 - 2 = 2
                        //h - 1 c - 3 -- 4 - 1 = 3
                        if (health > 0)
                        {
                            var sprites = _configuration.obstacles[0].sprites;
                            obj.GetComponent<SpriteRenderer>().sprite = _configuration.obstacles[0].sprites[sprites.Length - health];
                            health--;
                        }
                        else
                        {
                            Object.Destroy(obj);
                            entity.Destroy();

                            entity = _world.NewEntity();
                            int randomNum = Random.Range(0, _configuration.blocks.Count);

                            obj = _world.spawnGameObject(
                                obstacleCoord,
                                entity,
                                _configuration.blocks[randomNum].prefab,
                                _configuration.blocks[randomNum].sprites[0]
                            );

                            entity.Get<Position>().value = obstacleCoord;
                            entity.Get<BlockType>().value = _configuration.blocks[randomNum].type;
                            entity.Get<Points>().value = _configuration.blocks[randomNum].points;
                            entity.Get<LinkToObject>().value = obj; //link to entity from gameobject

                            board[obstacleCoord] = entity;
                        }
                    }
                }
            }
        }
    }
}