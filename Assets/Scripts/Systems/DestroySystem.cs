using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class DestroySystem : IEcsRunSystem
    {
        private EcsFilter<DestroyEvent, SpawnType, LinkToObject> _filter;
        private GameState _gameState;
        private Configuration _configuration;
        private EcsWorld _world;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var board = _gameState.Board;

                foreach (int index in _filter)
                {
                    ref var spawnType    = ref _filter.Get2(index).value;
                    ref var linkToObject = ref _filter.Get3(index).value;

                    var explosion = Object.Instantiate(_configuration.deathVFX, linkToObject.transform.position, linkToObject.transform.rotation);
                    Object.Destroy(explosion, _configuration.durationOfExplosion);

                    if (spawnType == BlockTypes.Default)
                    {
                        _filter.GetEntity(index).Get<SpawnEvent>();
                        _filter.GetEntity(index).Get<SpawnType>().value = spawnType;

                        Object.Destroy(linkToObject); //удаляем обжект на сцене
                    }
                    else
                    {
                        _filter.GetEntity(index).Get<SpawnEvent>();
                        _filter.GetEntity(index).Get<SpawnType>().value = spawnType;

                        Object.Destroy(linkToObject); //удаляем обжект на сцене
                    }
                }
            }
        }
    }
}