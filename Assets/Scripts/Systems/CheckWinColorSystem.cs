using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class CheckWinColorSystem : IEcsSystem
    {
        private EcsFilter<DestroyEvent, SpawnType, LinkToObject> _filter;
       
        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                foreach (int index in _filter)
                {
                    ref var spawnType = ref _filter.Get2(index).value;
                    
                }
            }
        }
    }    
}