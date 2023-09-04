using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class AnimationPlaySystem : IEcsRunSystem
    {
        private EcsFilter<MoveBlockedEvent, Position> _filter;
        private GameState _gameState;

        public void Run()
        {
            var board = _gameState.Board;
            ref var position = ref _filter.Get2(0).value;

            if (!_filter.IsEmpty())
            {
                if (board[position].Get<BlockType>().value != BlockTypes.Obstacle)
                {
                    var entity = board[position].Get<LinkToObject>().value;
                    entity.transform.DOShakePosition(1f);
                    
                }

            }
        }
    }
}
