using Leopotam.Ecs;
using Match3;

public class HandleBoosterDestroyBlocksInLineSystem : IEcsRunSystem
{
    private EcsFilter<HandleBoosterEvent, BlockType> _filter;
    private GameState _gameState;


    public void Run()
    {
        if (!_filter.IsEmpty())
        {
            ref var boosterType = ref _filter.Get1(1).boosterType;
            ref var blockType = ref _filter.Get2(0).value;

            if (boosterType == BoosterTypes.DestroyBlocksInLine)
            {
               
            }
        }

    }
}
