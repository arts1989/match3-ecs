using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Match3
{
    internal class LinkToEntity : MonoBehaviour //, IDropHandler
    {
        public EcsEntity entity;

        //public void OnDrop(PointerEventData eventData)
        //{
        //    Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!");


        //   // ref var type = ref entity.Get<BlockType>().value;
        //   // Debug.Log("ent id: " + entity.GetInternalId() + " type: " + type);

        //}
    }
}