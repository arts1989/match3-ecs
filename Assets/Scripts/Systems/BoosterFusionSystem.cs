using Leopotam.Ecs;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Match3
{
    internal class BoosterFusionSystem : IEcsRunSystem
    {
        private EcsFilter<BoosterFusionEvent, LinkToObject, BlockType> _filter;
        private GameState _gameState;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var board = _gameState.Board;

                ref var obj1 = ref _filter.Get2(0).value;
                ref var obj2 = ref _filter.Get2(1).value;
                ref var Type1 = ref _filter.Get3(0).value;
                ref var Type2 = ref _filter.Get3(1).value;

                var entity1 = _filter.GetEntity(0);
                var entity2 = _filter.GetEntity(1);

                var result = board.boosterFusion(ref Type1,ref Type2);



            }

        }
    }
}