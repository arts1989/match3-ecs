using Leopotam.Ecs;
using UnityEngine;


namespace Match3
{
    internal class BackgroundInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private Configuration _configuration;
        public void Init()
        {
            foreach (var background in _configuration.levels)
            {
                //var entity = _world.NewEntity();
                //var obj = Object.Instantiate(background.background);
                //obj.transform.SetParent(GameObject.Find("Background").transform);
                //obj.AddComponent<LinkToEntity>().entity = entity;
                //entity.Get<LinkToObject>().value = obj;
            }
        }
    }
}