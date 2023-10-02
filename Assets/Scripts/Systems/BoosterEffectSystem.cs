using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class BoosterEffectSystem : IEcsRunSystem
    {
        private EcsFilter<BoosterActivationEvent, Position, CheckMoveEvent> _filter;

        private GameState _gameState;
        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var board = _gameState.Board;

                foreach (var index in _filter)
                {
                    ref var position = ref _filter.Get2(index).value;
                    ref var direction = ref _filter.Get3(index).direction;

                    switch (board.checkBoosterType(ref position))
                    {
                        case 1:
                            // Debug.Log("Полосатая Горизонт");
                            foreach (var target in board.boosterLineX(ref position))
                            {
                                board[target].Get<SpawnType>().value = BlockTypes.Default;
                                board[target].Get<DestroyEvent>();
                            }
                            break;
                        case 2:
                            // Debug.Log("Полосатая Верикаль");
                            foreach (var target in board.boosterLineY(ref position))
                            {
                                board[target].Get<SpawnType>().value = BlockTypes.Default;
                                board[target].Get<DestroyEvent>();
                            }

                            break;
                        case 3:
                            // Debug.Log("Крест");
                            foreach (var target in board.boosterCross(ref position))
                            {
                                board[target].Get<SpawnType>().value = BlockTypes.Default;
                                board[target].Get<DestroyEvent>();
                            }

                            break;
                        case 4:
                            // Debug.Log("Бомба");
                            foreach (var target in board.boosterBombS(ref position))
                            {
                                board[target].Get<SpawnType>().value = BlockTypes.Default;
                                board[target].Get<DestroyEvent>();
                            }
                            break;
                        case 5:
                            //  Debug.Log("Божья коровка");
                            foreach (var target in board.boosterHoming(ref position))
                            {
                                board[target].Get<SpawnType>().value = BlockTypes.Default;
                                board[target].Get<DestroyEvent>();
                            }
                            break;
                        default:
                            board[position].Get<SpawnType>().value = BlockTypes.Default;
                            board[position].Get<DestroyEvent>();
                            break;

                    }



                }


            }
        }



    }
}
