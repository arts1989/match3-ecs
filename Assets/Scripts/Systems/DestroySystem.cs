﻿using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class DestroySystem : IEcsRunSystem
    {
        private EcsFilter<DestroyEvent, LinkToObject, Position> _filter;

        public void Run()
        {
            foreach(int index in _filter)
            {
                ref var obj = ref _filter.Get2(index).value;
                Object.Destroy(obj);
                _filter.GetEntity(index).Get<SpawnEvent>();
                //_filter.GetEntity(index).Destroy();
            }
        }
    }
}