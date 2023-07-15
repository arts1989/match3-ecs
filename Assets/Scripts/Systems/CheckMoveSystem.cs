﻿using Leopotam.Ecs;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Match3
{
    internal class CheckMoveSystem : IEcsRunSystem
    {
        private EcsFilter<CheckMoveEvent, LinkToObject, Position> _filter;

        public void Run()
        {
            //система для проверки можно ли двигать гем в направлении
            //вектора  - в соседний ряд в комбинацию или оставить на месте.
            if (!_filter.IsEmpty())
            {
                ref var moveVector = ref _filter.Get1(0).moveVector;
                ref var position = ref _filter.Get3(0).value;

                if (Physics.Raycast(position, moveVector, out var hitInfo))
                {
                    var entity = hitInfo.collider.GetComponent<LinkToEntity>();
                    if (entity)
                    {
                        _filter.GetEntity(0).Get<MoveEvent>();
                        entity.entity.Get<MoveEvent>();
                    } 
                }
            }
        }
    }
}