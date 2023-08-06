using Leopotam.Ecs;

namespace Match3
{
    internal partial class CheckLoseSystem : IEcsRunSystem
    {
        private EcsFilter<LoseEvent> _filter;
        private SceneData _sceneData;

        public void Run()
        {
            foreach (var index in _filter)
            {
                _filter.GetEntity(index).Del<LoseEvent>();
                _sceneData.UI.LoseScreen.Show(true);
            }
        }
    }
}