using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class DestroySystem : IEcsRunSystem
    {
        private EcsFilter<DestroyEvent, LinkToObject, Points> _filter;
        private GameState _gameState;
        private Configuration _configuration;

        public void Run()
        {
            foreach(int index in _filter)
            {
                ref var obj    = ref _filter.Get2(index).value;
                ref var points = ref _filter.Get3(index).value;
                _gameState.PointsScored += points;

                Object.Destroy(obj);
                var explosion = Object.Instantiate(_configuration.deathVFX, obj.transform.position, obj.transform.rotation);
                Object.Destroy(explosion, _configuration.durationOfExplosion);

                _filter.GetEntity(index).Del<BlockType>();
                _filter.GetEntity(index).Del<LinkToObject>();
                _filter.GetEntity(index).Del<Points>();

                _filter.GetEntity(index).Get<SpawnEvent>();
                //_filter.GetEntity(index).Destroy();
            }
        }
      }
}