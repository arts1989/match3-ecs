using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class BoosterEffectSystem : IEcsRunSystem
    {
        private EcsFilter<BoosterActivationEvent, Position, BlockType> _filter;

        private GameState _gameState;
        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var board = _gameState.Board;

                foreach (var index in _filter)
                {
                    ref var position = ref _filter.Get2(index).value;
                    ref var type = ref _filter.Get3(index).value;

                    var coords = board.boosterActivation(ref position, ref type);

                    foreach (var target in coords)
                    {
                        board[target].Get<SpawnType>().value = BlockTypes.Default;
                        board[target].Get<DestroyEvent>();
                    }



                }


            }
        }



    }
}
