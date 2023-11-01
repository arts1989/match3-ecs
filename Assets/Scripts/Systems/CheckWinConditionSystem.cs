using Leopotam.Ecs;

namespace Match3
{
    internal class CheckWinConditionSystem : IEcsRunSystem
    {
        private EcsFilter<DestroyEvent, BlockType> _filter;
        private GameState _gameState;
        private SceneData _sceneData;

        public void Run()
        {
            if (_filter.IsEmpty()) return;
            foreach (var index in _filter)
            {
                ref var blockType = ref _filter.Get2(index).value;

                switch (blockType)
                {
                    case BlockTypes.Blue when _gameState.LevelType == LevelTypes.OnlyBlue:
                    case BlockTypes.Red when _gameState.LevelType == LevelTypes.OnlyRed:
                    case BlockTypes.Purple when _gameState.LevelType == LevelTypes.OnlyPurple:
                    case BlockTypes.Green when _gameState.LevelType == LevelTypes.OnlyGreen:
                    case BlockTypes.Yellow when _gameState.LevelType == LevelTypes.OnlyYellow:
                        _gameState.TargetWinLevel--;

                        if (_gameState.TargetWinLevel <= 0)
                            _sceneData.UI.WinScreen.Show(true);

                        if (_gameState.MovesAvaliable <= 0 && _gameState.TargetWinLevel <= 0)
                            _sceneData.UI.LoseScreen.Show(true);
                        break;
                }
            }
        }
    }
}