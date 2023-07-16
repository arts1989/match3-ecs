using Leopotam.Ecs;
using DG.Tweening;
using UnityEngine;

namespace Match3
{
    internal class MoveSystem : IEcsRunSystem
    {
        private EcsFilter<MoveEvent, LinkToObject, Position> _filter;
        private EcsWorld _world;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                ref var Object1 = ref _filter.Get2(0).value;
                ref var Object2 = ref _filter.Get2(1).value; 

                var Object1Pos = Object1.transform.position;
                var Object2Pos = Object2.transform.position;

                // меняем трансформ у обжектов
                Object1.transform.DOMove(Object2Pos, .5f)
                    .OnComplete(() => { 
                        Debug.Log("animation 1 complete");
                        if (_filter.GetEntity(0).Get<Cell>().type == Types.Blue)
                        {
                            _filter.GetEntity(0).Get<DestroyEvent>();
                        }
                    });
                Object2.transform.DOMove(Object1Pos, .5f)
                    .OnComplete(() => { 
                        Debug.Log("animation 2 complete");
                        if (_filter.GetEntity(1).Get<Cell>().type == Types.Blue)
                        {
                            _filter.GetEntity(1).Get<DestroyEvent>();
                        }
                    });

                // меняем position компонент у ентити
                ref var Entity1Pos = ref _filter.Get3(0).value;
                ref var Entity2Pos = ref _filter.Get3(1).value;

                Entity1Pos = Object2Pos;
                Entity2Pos = Object1Pos;

                //Debug.Log("swapped");
            }
        }
    }
}