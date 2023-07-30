using Leopotam.Ecs;
using System;
using System.Reflection;
using UnityEngine;

namespace Match3
{
    internal class HandleBoosterSystem : IEcsRunSystem
    {
        private EcsFilter<HandleBoosterEvent, BlockType> _filter;
        private GameState _gameState;

        public void Run()
        {
            if(!_filter.IsEmpty())
            {
                ref var boosterType = ref _filter.Get1(0).boosterType;
                ref var blockType   = ref _filter.Get2(0).value;

                if (boosterType == BoosterTypes.DestroyBlocksSameType)
                {
                    foreach(var entity in _gameState.Board.Values)
                    {
                        ref var currentBlockType = ref entity.Get<BlockType>().value;
                        if(currentBlockType == blockType)
                        {
                            entity.Get<DestroyEvent>();
                        }
                    }
                }
            }
        }
    }
}