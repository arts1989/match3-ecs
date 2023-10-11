using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    public static class EcsWordExtensions
    {
        public static GameObject spawnGameObject(this EcsWorld world, Vector2Int position, EcsEntity entity, Sprite sprite)
        {
            var name = position.x + "_" + position.y + "_" + sprite.name;
            var obj = new GameObject(name);
           
            obj.AddComponent<SpriteRenderer>().sprite = sprite;
            obj.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(0.175f, 0.175f);
            obj.AddComponent<LinkToEntity>().entity = entity; //link from gameobject to entity
            obj.AddComponent<BoxCollider>();
            obj.transform.position = new Vector3(position.x, position.y);

            var parent = GameObject.Find("Blocks");
            obj.transform.parent = parent.transform;

            return obj;
        }

        public static void spawnBlocksParent(this EcsWorld world)
        {
            var obj = new GameObject("Blocks");
        }
    }
}