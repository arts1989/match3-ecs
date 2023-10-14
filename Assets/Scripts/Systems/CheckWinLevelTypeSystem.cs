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

                    if (blockType == BlockTypes.Blue && _gameState.LevelType == LevelTypes.OnlyBlue)
                    {
                        _gameState.PointsScored += points;
                    }
                    if (blockType == BlockTypes.Red && _gameState.LevelType == LevelTypes.OnlyRed)
                    {
                        _gameState.PointsScored += points;
                    }
                    if (blockType == BlockTypes.Purple && _gameState.LevelType == LevelTypes.OnlyPurple)
                    {                        
                        _gameState.PointsScored += points;
                    }
                    if (blockType == BlockTypes.Green && _gameState.LevelType == LevelTypes.OnlyGreen)
                    {                        
                        _gameState.PointsScored += points;
                    }
                    if (blockType == BlockTypes.Yellow && _gameState.LevelType == LevelTypes.OnlyYellow)
                    {                        
                        _gameState.PointsScored += points;
                    }
                }
            }
        }
    }
}
