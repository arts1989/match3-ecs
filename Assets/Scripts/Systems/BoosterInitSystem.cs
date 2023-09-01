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
            float offsetX = 0;
            foreach (var booster in _configuration.UIBoosters)
            {
                var entity = _world.NewEntity();
                
                var obj = Object.Instantiate(booster.prefab);
                obj.transform.SetParent(GameObject.Find("BoosterWidget").transform);
                obj.AddComponent<LinkToEntity>().entity = entity; //link from gameobject to entity
                obj.AddComponent<UIBooster>();

                var rectTransform = obj.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(1f, 1f);
                rectTransform.localPosition = new Vector2(offsetX, 0);

                entity.Get<LinkToObject>().value = obj;
                entity.Get<BoosterType>().value = booster.boosterType;

                offsetX += 60f;
            }
        }
    }
}