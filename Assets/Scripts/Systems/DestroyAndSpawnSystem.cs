using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Match3
{
    internal class DestroyAndSpawnSystem : IEcsRunSystem
    {
        private EcsFilter<DestroyAndSpawnEvent, LinkToObject, Points, Position, BlockType> _filter;
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
                    ref var spawnBlockType = ref _filter.Get1(index).value;
                    ref var linkToObject   = ref _filter.Get2(index).value;
                    ref var points         = ref _filter.Get3(index).value;
                    ref var position       = ref _filter.Get4(index).value;   
                    ref var blockType      = ref _filter.Get5(index).value;
                
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

                    _gameState.PointsScored += points;
                    Object.Destroy(linkToObject);

                    var explosion = Object.Instantiate(_configuration.deathVFX, linkToObject.transform.position, linkToObject.transform.rotation);
                    Object.Destroy(explosion, _configuration.durationOfExplosion);

                    if (spawnBlockType == BlockTypes.Default)
                    {
                        int randomNum = Random.Range(0, _configuration.blocks.Count);
                        var newBlockType = _configuration.blocks[randomNum].type;

                        while (board.hasNearbySameType(ref position, ref newBlockType, true))
                        {
                            randomNum = Random.Range(0, _configuration.blocks.Count);
                            newBlockType = _configuration.blocks[randomNum].type;
                        }

                        var obj = _world.spawnGameObject(
                            position,
                            _filter.GetEntity(index),
                            _configuration.blocks[randomNum].prefab,
                            _configuration.blocks[randomNum].sprites[0]
                        );

                        linkToObject = obj;
                        blockType    = _configuration.blocks[randomNum].type;
                        points       = _configuration.blocks[randomNum].points;

                    }
                    else
                    {
                        foreach(var booster in _configuration.boosters)
                        {
                            if(booster.type == spawnBlockType)
                            {
                                var obj = _world.spawnGameObject(
                                    position,
                                    _filter.GetEntity(index),
                                    booster.prefab,
                                    booster.sprites[0]
                                );

                                linkToObject = obj;
                                blockType = booster.type;
                                points = booster.points;
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

                _world.NewEntity().Get<UpdateScoreEvent>();
            }
        }
    }
}