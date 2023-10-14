using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class BoosterMergeSystem : IEcsRunSystem
    {
        private EcsFilter<MoveEvent, LinkToObject, BlockType> _filter;
        private GameState _gameState;
        private EcsWorld _world;
        private Configuration _configuration;

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

                    obj1.transform.DOMove(obj2.transform.position, .25f).OnComplete(() => {
                        
                       
                        entity1.Get<SpawnType>().value = BlockTypes.Default;
                        entity1.Get<DestroyEvent>();

                        foreach (var configBlock in _configuration.boosters)
                        {
                            if (mergeType == configBlock.type)
                            {
                                Object.Destroy(entity2.Get<LinkToObject>().value);

                                var obj = _world.spawnGameObject(entity2.Get<Position>().value, entity2, configBlock.sprites[0]);

                                entity2.Get<BlockType>().value = mergeType;
                                entity2.Get<Points>().value = configBlock.points;
                                entity2.Get<LinkToObject>().value = obj; //link to entity from gameobject
                            }
                        }

                        _gameState.freezeBoard = false;
                    });
                }
            }
        }
    }
}