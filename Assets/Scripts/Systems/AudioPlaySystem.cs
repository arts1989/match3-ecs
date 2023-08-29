using Leopotam.Ecs;

namespace Match3
{
    internal class AudioPlaySystem : IEcsRunSystem
    {
        private EcsFilter<MoveEvent> _moveEvent;
        private EcsFilter<DestroyEvent> _destroyEvent;
        private EcsFilter<SpawnEvent> _spawnEvent;

        private SceneData _sceneData;

        public void Run()
        {
            if (!_moveEvent.IsEmpty())
            {
                _sceneData.swipeSound.Play();
                _sceneData.swipeSound.volume = 0.5f;
            }

            if (!_destroyEvent.IsEmpty())
            {
                _sceneData.destroyBlockSound.Play();
                _sceneData.destroyBlockSound.volume = 1f;
            }

            if (!_spawnEvent.IsEmpty())
            {
                _sceneData.destroyBlockSound.Play();
                _sceneData.destroyBlockSound.volume = 1f;
            }
        }
    }
}