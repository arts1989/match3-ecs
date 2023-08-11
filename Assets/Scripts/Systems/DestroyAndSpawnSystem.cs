using Leopotam.Ecs;
using UnityEngine;

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
            foreach(int index in _filter)
            {
                ref var SpawnBlockType = ref _filter.Get1(index).value;
                ref var LinkToObject   = ref _filter.Get2(index).value;
                ref var Points         = ref _filter.Get3(index).value;
                ref var Position       = ref _filter.Get4(index).value;   
                ref var BlockType      = ref _filter.Get5(index).value;

                _gameState.PointsScored += Points;

                Object.Destroy(LinkToObject);

                var explosion = Object.Instantiate(_configuration.deathVFX, LinkToObject.transform.position, LinkToObject.transform.rotation);
                Object.Destroy(explosion, _configuration.durationOfExplosion);

                if (SpawnBlockType == BlockTypes.Default)
                {
                    int randomNum = Random.Range(0, _configuration.blocks.Count);
                    var obj = Object.Instantiate(_configuration.blocks[randomNum].sprite);

                    obj.AddComponent<LinkToEntity>().entity = _filter.GetEntity(index); //link from gameobject to entity
                    obj.AddComponent<BoxCollider>();
                    obj.transform.position = new Vector2(
                        Position.x + _configuration.offset.x * Position.x,
                        Position.y + _configuration.offset.y * Position.y
                    );

                    LinkToObject = obj;
                    BlockType    = _configuration.blocks[randomNum].type;
                    Points       = _configuration.blocks[randomNum].points;

                }
                else if (SpawnBlockType == BlockTypes.Teewee)
                {
                    foreach(var booster in _configuration.boosters)
                    {
                        if(booster.type == BlockTypes.Teewee)
                        {
                            var obj = Object.Instantiate(booster.sprite);

                            obj.AddComponent<LinkToEntity>().entity = _filter.GetEntity(index); //link from gameobject to entity
                            obj.AddComponent<BoxCollider>();
                            obj.transform.position = new Vector2(
                                Position.x + _configuration.offset.x * Position.x,
                                Position.y + _configuration.offset.y * Position.y
                            );

                            LinkToObject = obj;
                            BlockType = booster.type;
                            Points = booster.points;
                        }
                    }
                }
                else if (SpawnBlockType == BlockTypes.Line)
                {
                    foreach (var booster in _configuration.boosters)
                    {
                        if (booster.type == BlockTypes.Line)
                        {
                            var obj = Object.Instantiate(booster.sprite);

                            obj.AddComponent<LinkToEntity>().entity = _filter.GetEntity(index); //link from gameobject to entity
                            obj.AddComponent<BoxCollider>();
                            obj.transform.position = new Vector2(
                                Position.x + _configuration.offset.x * Position.x,
                                Position.y + _configuration.offset.y * Position.y
                            );

                            LinkToObject = obj;
                            BlockType = booster.type;
                            Points = booster.points;
                        }
                    }
                }
                else if (SpawnBlockType == BlockTypes.Square)
                {
                    foreach (var booster in _configuration.boosters)
                    {
                        if (booster.type == BlockTypes.Square)
                        {
                            var obj = Object.Instantiate(booster.sprite);

                            obj.AddComponent<LinkToEntity>().entity = _filter.GetEntity(index); //link from gameobject to entity
                            obj.AddComponent<BoxCollider>();
                            obj.transform.position = new Vector2(
                                Position.x + _configuration.offset.x * Position.x,
                                Position.y + _configuration.offset.y * Position.y
                            );

                            LinkToObject = obj;
                            BlockType = booster.type;
                            Points = booster.points;
                        }
                    }
                }
            }

            if (!_filter.IsEmpty())
            {
                _world.NewEntity().Get<UpdateScoreEvent>();
            }
        }
    }
}