using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class SpawnSystem : IEcsRunSystem
    {
        private EcsFilter<SpawnEvent, LinkToObject, Position> _filter;
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
                var gem = Object.Instantiate(_configuration.blocks[randomNum].sprite);

                gem.AddComponent<LinkToEntity>().entity = _filter.GetEntity(index); //link from gameobject to entity
                gem.AddComponent<BoxCollider>();
                gem.transform.position = new Vector3(
                    position.x + _configuration.offset.x * position.x,
                    position.y + _configuration.offset.y * position.y
                );

                _filter.Get2(index).value = gem;
            }

            if(!_filter.IsEmpty())
            {
                _world.NewEntity().Get<UpdateScoreEvent>();
            }
        }
    }
}