using DG.Tweening;
using Leopotam.Ecs;
using Unity.VisualScripting;

namespace Match3
{
    internal class BoosterMergeSystem : IEcsRunSystem
    {
        private EcsFilter<MoveEvent, LinkToObject, BlockType> _filter;
        private GameState _gameState;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var board = _gameState.Board;

                ref var blockType1 = ref _filter.Get3(0).value;
                ref var blockType2 = ref _filter.Get3(1).value;

                board.getBoosterMergeType(ref blockType1, ref blockType2, out BlockTypes mergeType);

                if (mergeType != BlockTypes.Default)
                {
                    ref var obj1 = ref _filter.Get2(0).value;
                    ref var obj2 = ref _filter.Get2(1).value;

                    _gameState.freezeBoard = true;
                    var entity1 = _filter.GetEntity(0);
                    var entity2 = _filter.GetEntity(1);

                    entity1.Del<MoveEvent>();
                    entity2.Del<MoveEvent>();

                    obj2.transform.DOMove(obj1.transform.position, .25f).OnComplete(() => {
                        _gameState.freezeBoard = false;
                        entity1.Get<SpawnType>().value = BlockTypes.Default;
                        entity1.Get<DestroyEvent>();

                        entity1.Get<SpawnType>().value = mergeType;
                        entity1.Get<DestroyEvent>();
                    });
                }
            }
        }
    }
}