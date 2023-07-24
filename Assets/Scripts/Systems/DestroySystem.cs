using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class DestroySystem : IEcsRunSystem
    {
        private EcsFilter<DestroyEvent, LinkToObject, Points> _filter;
        private GameState _gameState;

        public void Run()
        {
            foreach(int index in _filter)
            {
                ref var obj = ref _filter.Get2(index).value;
                ref var points = ref _filter.Get3(index).value;
                _gameState.PointsScored += points;

                Object.Destroy(obj);
                _filter.GetEntity(index).Get<SpawnEvent>();
                //_filter.GetEntity(index).Destroy();
            }
        }
    }
}