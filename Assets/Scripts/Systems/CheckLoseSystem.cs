using Leopotam.Ecs;

namespace Match3
{
    internal partial class CheckLoseSystem : IEcsRunSystem
    {
        private EcsFilter<LoseEvent> _filter;
        private SceneData _sceneData;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                _sceneData.UI.LoseScreen.Show(true);
            }
        }
    }
}