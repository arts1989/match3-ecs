using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    public class AudioPlaySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<MoveEvent> _moveEvent;
        private EcsFilter<DestroyEvent> _destroyEvent;
        private EcsFilter<SpawnEvent> _spawnEvent;
        private EcsFilter<DenyEvent> _denyEvent;

        private SceneData _sceneData;
        private GameState _gameState;
        
        //private SliderButton _sliderButton; !!!

        public void Init()
        {
            var backgroundMusic = _gameState.backgroundAudioClip;
            _sceneData.backgroundMusic.clip = backgroundMusic;
            _sceneData.backgroundMusic.Play();
            _sceneData.backgroundMusic.volume = 0.2f;
            _sceneData.backgroundMusic.loop = true;
        }

        public void Run()
        {
            bool moveEvent    = _moveEvent.IsEmpty(), 
                 destroyEvent = _destroyEvent.IsEmpty(),
                 spawnEvent   = _spawnEvent.IsEmpty(),
                 denyEvent    = _denyEvent.IsEmpty();

            if (!moveEvent || !destroyEvent || !spawnEvent || !denyEvent) 
            {
                var blocksAudio = _sceneData.blocksAudio.GetComponent<AudioSource>();

                blocksAudio.clip =
                    !moveEvent    ? _gameState.swipeSound :
                    !destroyEvent ? _gameState.destroySound :
                    !spawnEvent   ? _gameState.spawnSound :
                    !denyEvent    ? _gameState.denySound :
                    null;

                blocksAudio.volume = 0.5f; //_sliderButton.slider.value;  //0.5f; !!!
                blocksAudio.Play();
            }
        }
    }
}