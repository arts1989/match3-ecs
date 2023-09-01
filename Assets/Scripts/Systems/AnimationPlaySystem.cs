using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    internal class AnimationPlaySystem : IEcsRunSystem
    {
        private EcsFilter<MoveBlockedEvent> _moveBlockedEvent;

        private SceneData _sceneData;
        private GameState _gameState;

        public void Run()
        {
            if(!_moveBlockedEvent.IsEmpty())
            {
                Debug.Log("fff");
            }
        }
    }
}
