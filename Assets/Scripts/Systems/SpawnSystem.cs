using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class SpawnSystem : IEcsRunSystem
    {
        private EcsFilter<SpawnEvent, LinkToObject, Position, BlockType, Points> _filter;
        private Configuration _configuration;
        private EcsWorld _world;

        public void Run()
        {
            // система генерации доски и ентитей - спавним префабы из конфига
            // и ставим геймобжекту ссылку на ентитю через компонент-монобех
            foreach(int index in  _filter)
            {
                ref var position = ref _filter.Get3(index).value;

                int randomNum = Random.Range(0, _configuration.blocks.Count);
                var obj = Object.Instantiate(_configuration.blocks[randomNum].sprite);

                obj.AddComponent<LinkToEntity>().entity = _filter.GetEntity(index); //link from gameobject to entity
                obj.AddComponent<BoxCollider>();
                obj.transform.position = new Vector3(
                    position.x + _configuration.offset.x * position.x,
                    position.y + _configuration.offset.y * position.y
                );
                
                _filter.Get2(index).value = obj;
                _filter.Get4(index).value = _configuration.blocks[randomNum].type;
                _filter.Get5(index).value = _configuration.blocks[randomNum].points;
            }

            if(!_filter.IsEmpty())
            {
                _world.NewEntity().Get<UpdateScoreEvent>();
            }
        }
    }
}