using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    public static class EcsWordExtensions
    {
        public static GameObject spawnGameObject(this EcsWorld world, Vector2Int position, EcsEntity entity, GameObject prefab, Sprite sprite)
        {
            var obj = Object.Instantiate(prefab);

            obj.AddComponent<SpriteRenderer>().sprite = sprite;
            obj.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(0.175f, 0.175f);
            obj.GetComponent<SpriteRenderer>().sortingLayerName = "Blocks";
            obj.GetComponent<SpriteRenderer>().sortingOrder = 5;

            obj.AddComponent<LinkToEntity>().entity = entity; //link from gameobject to entity
            obj.AddComponent<BoxCollider>();
            obj.transform.position = new Vector3(position.x, position.y);

            return obj;
        }
    }
}