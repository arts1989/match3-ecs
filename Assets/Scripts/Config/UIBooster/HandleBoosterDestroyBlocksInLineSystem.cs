using Leopotam.Ecs;
using Match3;
using UnityEngine;

public class HandleBoosterDestroyBlocksInRowSystem : IEcsRunSystem
{
    private EcsFilter<HandleBoosterEvent, BlockType> _filter;
    private GameStates _gameState;

    public void Run()
    {

        if (!_filter.IsEmpty())
        {
            ref var boosterType = ref _filter.Get1(1).boosterType;
            ref var blockType = ref _filter.Get2(0).value;

            if (boosterType == BoosterTypes.DestroyBlocksInRow)
            {
                foreach (var entity in _gameState.Board.Values)
                {
                    ref var currentBlockType = ref entity.Get<BlockType>().value;

                    for (int y = _gameState.Rows; y <= _gameState.Columns; y++)
                    {
                        entity.Get<DestroyAndSpawnEvent>().value = BlockTypes.Default;
     
                    }
                }
            }
        }
    }
}
