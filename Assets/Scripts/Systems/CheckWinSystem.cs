using Leopotam.Ecs;

namespace Match3
{
    internal partial class CheckWinSystem : IEcsRunSystem
    {
        private EcsFilter<WinEvent> _filter;
        private SceneData _sceneData;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                _sceneData.UI.WinScreen.Show(true);
            }
        }
    }
} 