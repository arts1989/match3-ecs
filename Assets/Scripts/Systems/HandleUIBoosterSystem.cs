using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using UnityEngine;

namespace Match3
{
    internal class HandleUIBoosterSystem : IEcsRunSystem
    {
        private EcsFilter<EcsUiEndDragEvent> _filter;
        private GameState _gameState;

        public void Run()
        {
            if(!_filter.IsEmpty())
            {
                var board = _gameState.Board;
                foreach (int index in _filter)
                {
                    ref var obj = ref _filter.Get1(index);

                    var worldPos = Camera.main.ScreenToWorldPoint(obj.Position);
                    var position = new Vector2Int((int) worldPos.x, (int) worldPos.y);

                    if (board.TryGetValue(position, out var entityDropped))
                    {
                        var linkToEntity = obj.Sender.GetComponent<LinkToEntity>();
                        
                        ref var boosterType = ref linkToEntity.entity.Get<BoosterType>().value;
                        ref var blockType = ref entityDropped.Get<BlockType>().value;

                        //destroy blocks same type
                        if (boosterType == BoosterTypes.DestroyBlocksSameType)
                        {
                            foreach (var entity in _gameState.Board.Values)
                            {
                                ref var currentBlockType = ref entity.Get<BlockType>().value;
                                if (currentBlockType == blockType)
                                {
                                    entity.Get<DestroyEvent>();
                                    entity.Get<SpawnType>().value = BlockTypes.Default;
                                }
                            }
                        }
                        //another booster
                    }
                }
            }
        }
     }
}