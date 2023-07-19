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
                ref var obj1 = ref _filter.Get2(0).value;
                ref var obj2 = ref _filter.Get2(1).value; 
                var tempObj1 = obj1.transform.position;
                
                // меняем трансформ у обжектов
                obj1.transform.DOMove(obj2.transform.position, .5f)
                    .OnComplete(() => { 
                        //Debug.Log("animation 1 complete");
                        //if (_filter.GetEntity(0).Get<BlockType>().value == BlockTypes.Blue)
                        //{
                        _filter.GetEntity(0).Get<DestroyEvent>();
                        //}
                    });
                obj2.transform.DOMove(tempObj1, .5f)
                    .OnComplete(() => {
                        //Debug.Log("animation 2 complete");
                        //if (_filter.GetEntity(1).Get<BlockType>().value == BlockTypes.Blue)
                        //{
                        _filter.GetEntity(1).Get<DestroyEvent>();
                        //}
                    });

                // меняем position компонент у ентити
                ref var pos1 = ref _filter.Get3(0).value;
                ref var pos2 = ref _filter.Get3(1).value;

                var tempPos1 = pos1;
                pos1 = pos2;
                pos2 = tempPos1;
            }
        }
    }
}