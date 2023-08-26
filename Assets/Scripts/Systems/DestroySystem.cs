using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class DestroySystem : IEcsRunSystem
    {
        private EcsFilter<DestroyEvent, SpawnType, LinkToObject> _filter;
        private GameState _gameState;
        private Configuration _configuration;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var board = _gameState.Board;

                foreach (int index in _filter)
                {
                    ref var spawnType    = ref _filter.Get2(index).value;
                    ref var linkToObject = ref _filter.Get3(index).value;

                    Object.Destroy(linkToObject); //удаляем обжект на сцене

                    var explosion = Object.Instantiate(_configuration.deathVFX, linkToObject.transform.position, linkToObject.transform.rotation);
                    Object.Destroy(explosion, _configuration.durationOfExplosion);

                    if (_gameState.waterfallSpawnEnable) //waterfall
                    {
                        _filter.GetEntity(index).Get<Waterfall>();
                        _filter.GetEntity(index).Get<SpawnType>().value = spawnType;
                    }
                    else
                    {
                        _filter.GetEntity(index).Get<Spawn>();
                        _filter.GetEntity(index).Get<SpawnType>().value = spawnType;
                    }
                }
            }
        }
    }
}