using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class BoosterInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private Configuration _configuration;

        public void Init()
        {
            foreach (var booster in _configuration.boosters)
            {
                var entity = _world.NewEntity();
                entity.Get<BoosterType>().value = booster.type;

                var obj = Object.Instantiate(booster.prefab);
                obj.transform.SetParent(GameObject.Find("BoosterWidget").transform);
                obj.AddComponent<LinkToEntity>().entity = entity; //link from gameobject to entity
                obj.AddComponent<UIItem>();
            }
        }
    }
}