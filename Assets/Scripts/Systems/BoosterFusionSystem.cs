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
        private Configuration _configuration;

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

                var result = board.boosterFusion(ref Type1, ref Type2);

                if (result != BlockTypes.Default)
                {
                    var sequence = DOTween.Sequence();
                    sequence.Insert(0, obj1.transform.DOMove(obj2.transform.position, .5f));

                    _gameState.freezeBoard = true;
                    sequence.Play().OnComplete(() =>
                    {
                        entity1.Get<SpawnType>().value = BlockTypes.Default;
                        entity1.Get<DestroyEvent>();
                        entity2.Get<BlockType>().value = result;
                        _gameState.freezeBoard = false;
                    });
                }
                else
                {
                    entity1.Get<MoveEvent>();
                    entity2.Get<MoveEvent>();
                }

            }

        }
    }
}