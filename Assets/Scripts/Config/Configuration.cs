using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu]
   
    public class Configuration : ScriptableObject
    {
        public int minChainLenght = 3;
        
        public List<Level> levels = new List<Level>();

        public List<Block> blocks = new List<Block>();

        public List<Block> boosters = new List<Block>();

        public List<Block> obstacles = new List<Block>();

        public List<Booster> UIBoosters = new List<Booster>();

        public List<Underlay> underlays = new List<Underlay>();

        public Vector2 offset;

        [Header("Effects")]
        public GameObject deathVFX;
        public float durationOfExplosion = 1f;

        [Header("Audio")]
        public AudioClip swipeSound;
        public AudioClip destroySound;
        public AudioClip spawnSound;


    }
}