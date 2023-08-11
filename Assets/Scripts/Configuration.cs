 using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Match3
{
    [CreateAssetMenu]
   
    public class Configuration : ScriptableObject
    {
        public int minChainLenght = 3;
        
        public List<Level> levels = new List<Level>();

        public List<Block> blocks = new List<Block>();

        public List<Block> boosters = new List<Block>();

        public List<Booster> UIBoosters = new List<Booster>();

        public Vector2 offset;

        [Header("Effects")]
        public GameObject deathVFX;
        public float durationOfExplosion = 1f;
    }
}