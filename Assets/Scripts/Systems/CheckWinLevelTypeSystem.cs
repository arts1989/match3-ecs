using Leopotam.Ecs;

namespace Match3 
{ 
    internal class CheckWinLevelTypeSystem : IEcsRunSystem
    {
        private EcsFilter<DestroyEvent, BlockType, Points> _filter;
        private GameState _gameState;       

        public void Run()
        {           
            if (!_filter.IsEmpty())
            {
                foreach (int index in _filter)
                {
                    ref var blockType = ref _filter.Get2(index).value;
                    ref var points = ref _filter.Get3(index).value;                    

                    if (blockType == BlockTypes.Blue && _gameState.LevelType == LevelType.OnlyBlue)
                    {
                        _gameState.PointsScored += points;
                    }
                    if (blockType == BlockTypes.Red && _gameState.LevelType == LevelType.OnlyRed)
                    {
                        _gameState.PointsScored += points;
                    }
                    if (blockType == BlockTypes.Purple && _gameState.LevelType == LevelType.OnlyPurple)
                    {                        
                        _gameState.PointsScored += points;
                    }
                    if (blockType == BlockTypes.Green && _gameState.LevelType == LevelType.OnlyGreen)
                    {                        
                        _gameState.PointsScored += points;
                    }
                    if (blockType == BlockTypes.Yellow && _gameState.LevelType == LevelType.OnlyYellow)
                    {                        
                        _gameState.PointsScored += points;
                    }
                }
            }
        }
    }
}
