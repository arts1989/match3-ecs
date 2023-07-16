using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

namespace Match3
{
    [CreateAssetMenu]
    public class Gem : ScriptableObject
    {
        public int PointsForDestroy = 5;
        public GameObject sprite;

        [SerializeField] public Types type;
    }
}