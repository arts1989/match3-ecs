using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class SpawnSystem : IEcsRunSystem
    {
        private EcsFilter<Spawn, SpawnType, Position> _filter;
        private GameState _gameState;
        private Configuration _configuration;
        private EcsWorld _world;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                if(!_gameState.freezeBoard) {
                    
                    var board = _gameState.Board;

                    foreach (int index in _filter)
                    {
                        var spawnType = _filter.Get2(index).value;
                        var position  = _filter.Get3(index).value;

                        if (spawnType == BlockTypes.Default)
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
                                _configuration.blocks[randomNum].sprites[0]
                            );

                            var explosion = Object.Instantiate(_configuration.deathVFX, obj.transform.position, obj.transform.rotation);
                            Object.Destroy(explosion, _configuration.durationOfExplosion);

                            _filter.GetEntity(index).Get<LinkToObject>().value = obj;
                            _filter.GetEntity(index).Get<BlockType>().value  = _configuration.blocks[randomNum].type;
                            _filter.GetEntity(index).Get<Points>().value = _configuration.blocks[randomNum].points;

                            _filter.GetEntity(index).Del<SpawnType>();
                            _filter.GetEntity(index).Del<Spawn>();

                            if(_gameState.waterfallSpawnEnable)  
                                _filter.GetEntity(index).Get<SpawnEvent>();
                        }
                        else
                        {
                            foreach(var booster in _configuration.boosters) 
                            {
                                if(booster.type == spawnType)
                                {
                                    var obj = _world.spawnGameObject(
                                        position,
                                        _filter.GetEntity(index),
                                        booster.sprites[0]
                                    );

                                    var explosion = Object.Instantiate(_configuration.deathVFX, obj.transform.position, obj.transform.rotation);
                                    Object.Destroy(explosion, _configuration.durationOfExplosion);

                                    _filter.GetEntity(index).Get<LinkToObject>().value = obj;
                                    _filter.GetEntity(index).Get<BlockType>().value = booster.type;
                                    _filter.GetEntity(index).Get<Points>().value = booster.points;

                                    _filter.GetEntity(index).Del<SpawnType>();
                                    _filter.GetEntity(index).Del<Spawn>();

                                    if (_gameState.waterfallSpawnEnable)
                                        _filter.GetEntity(index).Get<SpawnEvent>();
                                }
                            }
                        }
                    }
                }
            } 
        }
    }
}