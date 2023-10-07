using Leopotam.Ecs;
using DG.Tweening;
using Unity.VisualScripting;

namespace Match3
{
    internal partial class MoveSystem : IEcsRunSystem
    {
        private EcsFilter<MoveEvent, LinkToObject, Position> _filter;
        private GameState _gameState;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var board = _gameState.Board;

                ref var obj1 = ref _filter.Get2(0).value;
                ref var obj2 = ref _filter.Get2(1).value;
                var tempObj1 = obj1.transform.position;

                var entity1 = _filter.GetEntity(0);
                var entity2 = _filter.GetEntity(1);

                var pos1 = entity1.Ref<Position>().Unref().value;
                var pos2 = entity2.Ref<Position>().Unref().value;

                var sequence = DOTween.Sequence();

                sequence.Insert(0, obj1.transform.DOMove(obj2.transform.position, .5f));
                sequence.Insert(0, obj2.transform.DOMove(tempObj1, .5f));

                entity1.Get<Position>().value = pos2;
                entity2.Get<Position>().value = pos1;

                _gameState.Board[pos1] = entity2;
                _gameState.Board[pos2] = entity1;

                _gameState.freezeBoard = true;
                sequence.Play().OnComplete(() =>
                {
                    if (board.isBooster(ref pos2))
                    {
                        _filter.GetEntity(1).Get<BoosterActivationEvent>();
                    }
                    else
                        _filter.GetEntity(1).Get<CheckMatchEvent>().oldPosition = pos2;
                    _filter.GetEntity(0).Get<CheckMatchEvent>().oldPosition = pos1;
                    _gameState.freezeBoard = false;
                });
            }
        }
    }
}